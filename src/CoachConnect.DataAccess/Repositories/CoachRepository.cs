using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;

public class CoachRepository : ICoachRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<CoachRepository> _logger;

    public CoachRepository(CoachConnectDbContext dbContext ,ILogger<CoachRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ICollection<Coach>> GetAllAsync(QueryObject query)
    {
        _logger.LogDebug("Getting coaches from db");

        var coaches = _dbContext.Coaches.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.FirstName))
        {
            coaches = coaches.Where(u => u.FirstName.Contains(query.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(query.LastName))
        {
            coaches = coaches.Where(u => u.LastName.Contains(query.LastName));
        }

        if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
        {
            coaches = coaches.Where(u => u.PhoneNumber.Contains(query.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            coaches = coaches.Where(u => u.Email.Contains(query.Email));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
            {
                coaches = query.IsDescending ? coaches.OrderByDescending(x => x.FirstName) : coaches.OrderBy(x => x.FirstName);
            }

            if (query.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
            {
                coaches = query.IsDescending ? coaches.OrderByDescending(x => x.LastName) : coaches.OrderBy(x => x.LastName);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await coaches
            .Skip(skipNumber)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public Task<Coach?> GetByIdAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public async Task<Coach?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting coach by email: {email} from db", email);

        var res = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Email.Equals(email));
        return res;
    }      

    public Task<Coach?> UpdateAsync(CoachId id, Coach coach)
    {
        throw new NotImplementedException();
    }

    public Task<Coach?> DeleteAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public async Task<Coach?> RegisterCoachAsync(Coach coach)
    {
        _logger.LogDebug("Adding coach: {coach} to db", coach.Email);

        await _dbContext.Coaches.AddAsync(coach);
        await _dbContext.SaveChangesAsync();

        return coach;
    }
}