using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;
public class TeamRepository : ITeamRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<TeamRepository> _logger;

    public TeamRepository(ILogger<TeamRepository> logger)
    {
        _logger = logger;
    }

    public async Task<Team?> DeleteAsync(TeamId id)
    {
        _logger.LogDebug("Deleting Team: {id} from db", id);

        var res = await _dbContext.Teams.FindAsync(id);
        if (res == null) return null;

        _dbContext.Teams.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<ICollection<Team>> GetAllAsync(TeamQuery teamQuery)
    {
        _logger.LogDebug("Getting Teams from db");

        var teams = _dbContext.Teams.AsQueryable();

        if (!string.IsNullOrWhiteSpace(teamQuery.TeamCity))
        {
            teams = teams.Where(g => g.TeamCity.Contains(teamQuery.TeamCity));
        }

        if (!string.IsNullOrWhiteSpace(teamQuery.TeamName))
        {
            teams = teams.Where(g => g.TeamName.Contains(teamQuery.TeamName));
        }

        if (teamQuery.TeamTime != null && teamQuery.TeamTime != DateTime.MinValue)
        {
            teams = teams.Where(g => g.Created == teamQuery.TeamTime);
        }
        if (!string.IsNullOrWhiteSpace(teamQuery.SortBy))
        {
            if (teamQuery.SortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
            {
                teams = teamQuery.IsDescending ? teams.OrderByDescending(x => x.TeamCity) : teams.OrderBy(x => x.TeamCity);
            }

            if (teamQuery.SortBy.Equals("Opponent name", StringComparison.OrdinalIgnoreCase))
            {
                teams = teamQuery.IsDescending ? teams.OrderByDescending(x => x.TeamName) : teams.OrderBy(x => x.TeamName);
            }
        }

        var skipNumber = (teamQuery.PageNumber - 1) * teamQuery.PageSize;

        return await teams
            .Skip(skipNumber)
            .Take(teamQuery.PageSize)
            .ToListAsync();
    }

    public Task<Team?> GetByCoachIdAsync(CoachId coachid)
    {
        throw new NotImplementedException();
    }

    public async Task<Team?> GetByIdAsync(TeamId id)
    {
        _logger.LogDebug("Getting team by id: {id} from db", id);

        return await _dbContext.Teams.FindAsync(id);
    }

    public Task<Team?> GetByTeamAsync(string team)
    {
        throw new NotImplementedException();
    }

    public async Task<Team?> RegisterTeamAsync(Team team)
    {
        _logger.LogDebug("Adding Game to DB");

        await _dbContext.Teams.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        return team;
    }

    public async Task<Team?> UpdateAsync(TeamId id, Team team)
    {
        _logger.LogDebug("Updating Team: {id} in db", id);

        var tm = await _dbContext.Teams.FirstOrDefaultAsync(g => g.Id.Equals(id));
        if (tm == null) return null;

        tm.TeamCity = string.IsNullOrEmpty(team.TeamCity) ? tm.TeamCity : team.TeamCity;
        tm.TeamName = string.IsNullOrEmpty(team.TeamName) ? tm.TeamName : team.TeamName;
        tm.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return tm;
    }
}
