﻿using CoachConnect.DataAccess.Data;
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

        //DateTime date = DateTime.Now;
        //var user = new User
        //{
        //    Id = 1, 
        //    Username = "TestUserName",
        //    FirstName = "Jan",
        //    LastName = "Jansen",
        //    PhoneNumber = "1234567890",
        //    Email = "abc@abc.no",
        //    HashedPassword = "12345abcabc",
        //    Salt = "1234567890",
        //    Created = date,
        //    Updated = date            
        //};

        //return Task.FromResult<ICollection<User>>(new List<User> { user });
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName));
        return user;
    }

    public Task<ICollection<User>> GetByLastNameAsync(string userLastName)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetByPlayerLastNameAsync(string playerLastName)
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