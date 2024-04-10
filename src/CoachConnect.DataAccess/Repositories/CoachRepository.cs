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

    public async Task<ICollection<Coach>> GetAllAsync(CoachQuery query)
    {
        _logger.LogDebug("Getting coaches from db");

        var coaches = _dbContext.Coaches.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.FirstName))
        {
            coaches = coaches.Where(u => u.FirstName.StartsWith(query.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(query.LastName))
        {
            coaches = coaches.Where(u => u.LastName.StartsWith(query.LastName));
        }

        if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
        {
            coaches = coaches.Where(u => u.PhoneNumber.StartsWith(query.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            coaches = coaches.Where(u => u.Email.StartsWith(query.Email));
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
            .Include(c => c.Teams) //Eagerly loading in
            .Skip(skipNumber)
            .Take(query.PageSize)
            .ToListAsync();
    }

    //public async Task<Coach?> GetByIdAsync(CoachId id)
    //{
    //    _logger.LogDebug("Getting coach by id: {id} from db", id);

    //    return await _dbContext.Coaches.FindAsync(id);
    //}

    public async Task<Coach?> GetByIdAsync(CoachId id)
    {
        _logger.LogDebug("Getting coach by id: {id} from db", id);

        return await _dbContext.Coaches.Include(t => t.Teams) //bruker eagerly loading
                                        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coach?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting coach by email: {email} from db", email);

        var res = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Email.Equals(email));
        return res;
    }      

    public async Task<Coach?> UpdateAsync(CoachId id, Coach coach)
    {
        _logger.LogDebug("Updating coach: {id} in db", id);

        var cch = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Id.Equals(id));
        if (cch == null) return null;

        cch.FirstName = string.IsNullOrEmpty(coach.FirstName) ? cch.FirstName : coach.FirstName;
        cch.LastName = string.IsNullOrEmpty(coach.LastName) ? cch.LastName : coach.LastName;
        cch.PhoneNumber = string.IsNullOrEmpty(coach.PhoneNumber) ? cch.PhoneNumber : coach.PhoneNumber;
        cch.Email = string.IsNullOrEmpty(coach.Email) ? cch.Email : coach.Email;
        cch.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();
        return cch;
    }

    public async Task<Coach?> DeleteAsync(CoachId id)
    {
        _logger.LogDebug("Deleting coach: {id} from db", id);

        var res = await _dbContext.Coaches.FindAsync(id);
        if (res == null) return null;

        _dbContext.Coaches.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<Coach?> RegisterCoachAsync(Coach coach)
    {
        _logger.LogDebug("Adding coach: {coach} to db", coach.Email);

        await _dbContext.Coaches.AddAsync(coach);

        var existingRoleAssignment = await _dbContext.Jwt_user_roles.FirstOrDefaultAsync(r => r.UserId.Equals(coach.Id.coachId) && r.RoleId == 2);
        if (existingRoleAssignment != null)
        {
            _logger.LogDebug("Could not add coach: {coach} already has this role", coach.Email);
            return null; 
        }

        JwtUserRole roleAssignment = new() // lager objekt og kjører inn i db
        {
            UserId = coach.Id.coachId,
            RoleId = 2
        };

        _dbContext.Jwt_user_roles.Add(roleAssignment);

        await _dbContext.SaveChangesAsync();

        return coach;
    }
}