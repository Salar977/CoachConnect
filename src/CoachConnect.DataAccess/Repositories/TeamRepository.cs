using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories;
public class TeamRepository : ITeamRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<TeamRepository> _logger;
    public Task<Team?> DeleteAsync(TeamId id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Team>> GetAllAsync(TeamQuery query)
    {
        throw new NotImplementedException();
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

    public Task<Team?> RegisterTeamAsync(Team coach)
    {
        throw new NotImplementedException();
    }

    public Task<Team?> UpdateAsync(TeamId id, Team team)
    {
        throw new NotImplementedException();
    }
}
