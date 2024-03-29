﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;
using Serilog.Core;

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

    public async Task<ICollection<UserDTO>> GetAllAsync(UserQuery userQuery)
    {
        _logger.LogDebug("Getting all users");
        var res = await _userRepository.GetAllAsync(userQuery);
        return res.Select(user => _userMapper.MapToDTO(user)).ToList();
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting user by id: {id}", id);

        var userId = new UserId(id);
        var res = await _userRepository.GetByIdAsync(userId);
        return res != null ? _userMapper.MapToDTO(res) : null;     
    }

    public async Task<UserDTO?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email: {email}", email);

        var res = await _userRepository.GetByEmailAsync(email);
        return res != null ? _userMapper.MapToDTO(res) : null;
    }  
    
    public async Task<UserDTO?> UpdateAsync(Guid id, UserDTO dto)
    {
        _logger.LogDebug("Updating user: {id}", id);

        // husk at users (el admin) kun skal kunne oppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
        // kanksje noe som : throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");

        var userId = new UserId(id);
        var user = _userMapper.MapToEntity(dto);
        user.Id = userId;

        var res = await _userRepository.UpdateAsync(userId, user);
        return res != null ? _userMapper.MapToDTO(user) : null;
    }

    public async Task<UserDTO?> DeleteAsync(Guid id)
    {
        // husk at users (el admin) kun skal kunne slette sin egen user. Dette må vel settes i JWT autorisering. Ikke glem må ha med dette.
        // kanksje noe som : throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");
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
            return null; // sette opp custom exception? user already exists. Returnerer nå bare BadRequesten fra controlleren.
        }

        var user = _userRegistrationMapper.MapToEntity(dto);

        user.Id = UserId.NewId; // Generate a new UserId. Må ha med for at UserID Guid skal fungere.
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, user.Salt);

        var res = await _userRepository.RegisterUserAsync(user);

        return res != null ? _userMapper.MapToDTO(res) : null;
    }
}