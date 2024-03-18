using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
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

    public async Task<ICollection<User>> GetAllAsync(UserQuery userQuery)
    {
        _logger.LogDebug("Getting users from db");

        var users = _dbContext.Users.AsQueryable();

        if(!string.IsNullOrWhiteSpace(userQuery.FirstName))
        {
            users = users.Where(u => u.FirstName.StartsWith(userQuery.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(userQuery.LastName))
        {
            users = users.Where(u => u.LastName.StartsWith(userQuery.LastName));
        }

        if (!string.IsNullOrWhiteSpace(userQuery.PhoneNumber))
        {
            users = users.Where(u => u.PhoneNumber.StartsWith(userQuery.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(userQuery.Email))
        {
            users = users.Where(u => u.Email.StartsWith(userQuery.Email));
        }

        if (!string.IsNullOrWhiteSpace(userQuery.SortBy))
        {
            if (userQuery.SortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
            {
                users = userQuery.IsDescending ? users.OrderByDescending(x => x.FirstName) : users.OrderBy(x => x.FirstName);
            }

            if (userQuery.SortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
            {
                users = userQuery.IsDescending ? users.OrderByDescending(x => x.LastName) : users.OrderBy(x => x.LastName);
            }
        }

        var skipNumber = (userQuery.PageNumber - 1) * userQuery.PageSize;

        return await users
            .Skip(skipNumber)
            .Take(userQuery.PageSize)
            .ToListAsync();
    }

    //public async Task<ICollection<User>> GetAllAsync(int page, int pageSize)
    //{
    //    _logger.LogDebug("Getting users from db");

    //    int itemsToSkip = (page - 1) * pageSize;

    //    var res = await _dbContext.Users
    //        .OrderBy(u => u.LastName)
    //        .Skip(itemsToSkip)
    //        .Take(pageSize)
    //        .Distinct()
    //        .ToListAsync();

    //    return res;
    //}


    public async Task<User?> GetByIdAsync(UserId id)
    {
        _logger.LogDebug("Getting user by id: {id} from db", id);

        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email: {email} from db", email);

        var res = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        return res;
    }

    //public async Task<ICollection<User>> GetByLastNameAsync(string userLastName)
    //{
    //    _logger.LogDebug("Getting user by lastname: {userLastName} from db", userLastName);

    //    var res = await _dbContext.Users
    //        .Where(u => u.LastName
    //        .StartsWith(userLastName))
    //        .OrderBy(u => u.LastName) // husk legge til sortere alfabetisk også på Coach
    //        .ToListAsync();     

    //    return res;
    //}  

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

        await _dbContext.SaveChangesAsync();
        return usr;
    }

    public async Task<User?> DeleteAsync(UserId id)
    {
        _logger.LogDebug("Deleting user: {id} from db", id);

        var res = await _dbContext.Users.FindAsync(id);
        if (res == null) return null;
        
        _dbContext.Users.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;               
    }

    public async Task<User?> RegisterUserAsync(User user)
    {
        _logger.LogDebug("Adding user: {user} to db", user.Email);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}