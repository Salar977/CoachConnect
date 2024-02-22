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

        var res = await _dbContext.Users
            .OrderBy(u => u.LastName)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();

        return res;
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        _logger.LogDebug("Getting user by id: {id} from db", id);

        var res = await _dbContext.Users.FindAsync(id);
        return res;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email: {email} from db", email);

        var res = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        return res;
    }

    public async Task<ICollection<User>> GetByLastNameAsync(string userLastName)
    {
        _logger.LogDebug("Getting user by lastname: {userLastName} from db", userLastName);

        var res = await _dbContext.Users
            .Where(u => u.LastName
            .Contains(userLastName))
            .OrderBy(u => u.LastName) // husk legge til sortere alfabetisk også på Coach
            .ToListAsync();     

        return res;
    }

    public async Task<User?> RegisterUserAsync(User user)
    {
        _logger.LogDebug("Adding user: {user} to db", user);
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> UpdateAsync(UserId id, User user)
    {
        _logger.LogDebug("Updating user: {id} in db", id);

        var usr = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        if (usr == null) return null;

        usr.FirstName = string.IsNullOrEmpty(user.FirstName) ? usr.FirstName : user.FirstName;
        usr.LastName = string.IsNullOrEmpty(user.LastName) ? usr.LastName : user.LastName;
        usr.PhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? usr.PhoneNumber : user.PhoneNumber;
        usr.Email = string.IsNullOrEmpty(user.Email) ? usr.Email : user.Email;
        usr.Updated = DateTime.Now;

        return usr;
    }

    public async Task<User?> DeleteAsync(UserId id)
    {
        _logger.LogDebug("Deleting user: {id} from db", id);

        await Task.Delay(10);
        throw new NotImplementedException();
    }  
}