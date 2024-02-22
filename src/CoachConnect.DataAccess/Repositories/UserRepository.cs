using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Esf;

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
            .OrderBy(u => u.LastName)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();       
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        _logger.LogDebug("Getting user by id from db");

        var res = await _dbContext.Users.FindAsync(id);
        return res;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email from db");

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        return user;
    }

    public async Task<ICollection<User>> GetByLastNameAsync(string userLastName)
    {
        return await _dbContext.Users
            .Where(u => u.LastName
            .Contains(userLastName))
            .OrderBy(u => u.LastName)
            .ToListAsync();     
    }

    public async Task<User?> RegisterUserAsync(User user)
    {
        _logger.LogDebug("Adding user to db");

        // Generate a new UserId, denne viste ikke yngve i videon derfor jeg fikk 000-0000-00 osv på Guid
        user.Id = UserId.NewId;
        
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