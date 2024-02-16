using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegistrationMapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, 
                       IMapper<User, UserDTO> userMapper,
                       IMapper<User, UserRegistrationDTO> userRegistrationMapper,
                       ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _userRegistrationMapper = userRegistrationMapper;
        _logger = logger;
    }
    public Task<UserDTO?> DeleteAsync(int id, int loggedInUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<UserDTO>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting all users");

        var res = await _userRepository.GetAllAsync(page, pageSize);
        var dtos = res.Select(user => _userMapper.MapToDTO(user)).ToList();
        return dtos;        
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