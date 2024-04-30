using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;

namespace CoachConnect.BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserCoachUpdateDTO> _userUpdateMapper;    
    private readonly IMapper<Player, PlayerDTO> _playerMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegistrationMapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, 
                       IMapper<User, UserDTO> userMapper,
                       IMapper<User, UserCoachUpdateDTO> userUpdateMapper,
                       IMapper<Player, PlayerDTO> playerMapper,
                       IMapper<User, UserRegistrationDTO> userRegistrationMapper,
                       ILogger<UserService> logger)
    {   
        _userRepository = userRepository;
        _userMapper = userMapper;
        _userUpdateMapper = userUpdateMapper;
        _playerMapper = playerMapper;
        _userRegistrationMapper = userRegistrationMapper;
        _logger = logger;
    }

    public async Task<ICollection<UserDTO>> GetAllAsync(UserQuery userQuery)
    {
        _logger.LogDebug("Getting all users");
        var users = await _userRepository.GetAllAsync(userQuery);

        var userDtos = users.Select(user =>
        {
            var playerDtos = user.Players.Select(player => _playerMapper.MapToDTO(player)).ToList();
            var userDto = _userMapper.MapToDTO(user);
            userDto.Players = playerDtos;
            return userDto;
        }).ToList();

        return userDtos;
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting user by id: {id}", id);

        var userId = new UserId(id);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            _logger.LogInformation("Could not get user by id -> user == null");
            return null;
        }

        var players = user.Players;
        var playerDtos = players.Select(player => _playerMapper.MapToDTO(player)).ToList();

        var userDto = _userMapper.MapToDTO(user);
      
        userDto.Players = playerDtos;

        return userDto;
    }

    public async Task<UserDTO?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email: {email}", email);

        var res = await _userRepository.GetByEmailAsync(email);
        return res != null ? _userMapper.MapToDTO(res) : null;
    }  
    
    public async Task<UserCoachUpdateDTO?> UpdateAsync(Guid id, UserCoachUpdateDTO dto)
    {
        _logger.LogDebug("Updating user: {id}", id);

        var userId = new UserId(id);
        var user = _userUpdateMapper.MapToEntity(dto);
        user.Id = userId;

        var res = await _userRepository.UpdateAsync(userId, user);
        return res != null ? _userUpdateMapper.MapToDTO(user) : null;
    }

    public async Task<UserDTO?> DeleteAsync(Guid id)
    {
        _logger.LogDebug("Deleting user: {id}", id);

        var userId = new UserId(id);
        var res = await _userRepository.DeleteAsync(userId);
        return res != null ? _userMapper.MapToDTO(res) : null;
    }

    public async Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new user: {email}", dto.Email);

        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            _logger.LogDebug("User already exists: {email}", dto.Email);
            return null;
        }

        var user = _userRegistrationMapper.MapToEntity(dto);

        user.Id = UserId.NewId; 
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, user.Salt);

        var res = await _userRepository.RegisterUserAsync(user);

        return res != null ? _userMapper.MapToDTO(res) : null;
    }
}