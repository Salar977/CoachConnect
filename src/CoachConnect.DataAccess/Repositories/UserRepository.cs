using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(CoachConnectDbContext dbContext, ILogger<UserRepository> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<ICollection<User>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting users from db");

        int itemsToSkip = (page - 1) * pageSize;

        return await _dbContext.Users
            .OrderBy(u => u.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();       
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));
        return user;
    }

    public Task<ICollection<User>> GetByLastNameAsync(string userLastname)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetByPlayerLastNameAsync(string playerLastname)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> RegisterUserAsync(User user)
    {
        _logger.LogDebug("Adding user to db");

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public Task<User?> UpdateAsync(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }  
}