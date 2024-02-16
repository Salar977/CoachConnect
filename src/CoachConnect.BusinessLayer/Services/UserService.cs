using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    public Task<UserDTO?> DeleteAsync(int id, int loggedInUserId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserDTO>> GetAllAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<int>? GetAuthenticatedIdAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetByLastNameAsync(string userLastName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetByPlayerAsync(string playerLastName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> RegisterAsync(UserRegistrationDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> UpdateAsync(int id, UserDTO dto, int loggedInUserId)
    {
        throw new NotImplementedException();
    }
}