﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<ICollection<UserDTO>> GetAllAsync(string? lastName, int page, int pageSize)
    {
        if (!string.IsNullOrEmpty(lastName))
        {
            _logger.LogDebug("Getting users by lastname: {lastName}", lastName);
            var res = await _userRepository.GetByLastNameAsync(lastName);
            var dtos = res.Select(user => _userMapper.MapToDTO(user)).ToList();
            return dtos;
        }
        else
        {
            _logger.LogDebug("Getting all users");

            var res = await _userRepository.GetAllAsync(page, pageSize);
            var dtos = res.Select(user => _userMapper.MapToDTO(user)).ToList();
            return dtos;
        }
    }
    
    public async Task<UserDTO?> GetByIdAsync(UserId id)
    {
        _logger.LogDebug("Getting user by id {id}", id);

        var res = await _userRepository.GetByIdAsync(id);
        return res != null ? _userMapper.MapToDTO(res) : null;     
    }

    public async Task<UserDTO?> GetUserByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user by email");

        var res = await _userRepository.GetUserByEmailAsync(email);
        return res != null ? _userMapper.MapToDTO(res) : null;
    }  

    public async Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new user");

        var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
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

        return _userMapper.MapToDTO(res!);
    }

    public async Task<UserDTO?> UpdateAsync(UserId id, UserDTO dto)
    {
        _logger.LogDebug("Updating user");

        // husk at users (el admin) kun skal kunne eoppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.

        var user = _userMapper.MapToEntity(dto);
        user.Id = id;

        var res = await _userRepository.UpdateAsync(id, user);
        return res != null ? _userMapper.MapToDTO(user) : null;
    }

    public async Task<UserDTO?> DeleteAsync(UserId id)
    {
        // husk at users (el admin) kun skal kunne slette sin egen user. Dette må vel settes i JWT autorisering. Ikke glem må ha med dette.

        throw new NotImplementedException();
    }   
}