﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IMapper<Coach, CoachDTO> _coachMapper;
    private readonly IMapper<Coach, UserCoachUpdateDTO> _coachUpdateMapper;
    private readonly IMapper<Team, TeamDTO> _teamMapper;
    private readonly IMapper<Coach, CoachRegistrationDTO> _coachRegistartionMapper;
    private readonly ILogger<CoachService> _logger;

    public CoachService(ICoachRepository coachRepository,
                        IMapper<Coach, CoachDTO> coachMapper,
                        IMapper<Coach, UserCoachUpdateDTO> coachUpdateMapper,
                        IMapper<Team, TeamDTO> teamMapper,
                        IMapper<Coach, CoachRegistrationDTO> coachRegistrationMapper,
                        ILogger<CoachService> logger)
    {
        _coachRepository = coachRepository;
        _coachMapper = coachMapper;
        _coachUpdateMapper = coachUpdateMapper;
        _teamMapper = teamMapper;
        _coachRegistartionMapper = coachRegistrationMapper;
        _logger = logger;
    }

    public async Task<ICollection<CoachDTO>> GetAllAsync(CoachQuery query)
    {
        _logger.LogDebug("Getting all coaches");
        var coaches = await _coachRepository.GetAllAsync(query);

        var coachDtos = coaches.Select(coach =>
        {
            var teamDtos = coach.Teams.Select(team => _teamMapper.MapToDTO(team)).ToList();
            var coachDto = _coachMapper.MapToDTO(coach);
            coachDto = coachDto with { Teams = coachDto.Teams.Concat(teamDtos).ToList() };
            return coachDto;
        }).ToList();

        return coachDtos;
    }

    public async Task<CoachDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting coach by id: {id}", id);

        var coachId = new CoachId(id);
        var coach = await _coachRepository.GetByIdAsync(coachId);
        if (coach == null) return null;

        var teams = coach.Teams.ToList();
        var teamDtos = teams.Select(team => _teamMapper.MapToDTO(team)).ToList();

        var coachDto = _coachMapper.MapToDTO(coach);
        //coachDto.Teams = teamDtos; // dersom dto er regular class og ikke record

        coachDto = coachDto with { Teams = coachDto.Teams.Concat(teamDtos).ToList() };

        return coachDto;
    }

    public async Task<CoachDTO?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting coach by email: {email}", email);

        var res = await _coachRepository.GetByEmailAsync(email);
        return res != null ? _coachMapper.MapToDTO(res) : null;
    }     

    public async Task<UserCoachUpdateDTO?> UpdateAsync(Guid id, UserCoachUpdateDTO dto)
    {
        _logger.LogDebug("Updating coach: {id}", id);

        // husk at coaches (el admin) kun skal kunne eoppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
        // kanksje noe som : throw new UnauthorizedAccessException($"Coach {loggedInUserId} has no access to delete coach {id}");

        var coachId = new CoachId(id);
        var coach = _coachUpdateMapper.MapToEntity(dto);
        coach.Id = coachId;

        var res = await _coachRepository.UpdateAsync(coachId, coach);
        return res != null ? _coachUpdateMapper.MapToDTO(coach) : null;
    }

    public async Task<CoachDTO?> DeleteAsync(Guid id)
    {
        // husk at coaches (el admin) kun skal kunne slette sin egen user. Dette må vel settes i JWT autorisering. Ikke glem må ha med dette.
        // kanksje noe som : throw new UnauthorizedAccessException($"Coach {loggedInUserId} has no access to delete coach {id}");
        _logger.LogDebug("Deleting coach: {id}", id);

        var coachId = new CoachId(id);
        var res = await _coachRepository.DeleteAsync(coachId);
        return res != null ? _coachMapper.MapToDTO(res) : null;
    }

    public async Task<CoachDTO?> RegisterCoachAsync(CoachRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new coach: {email}", dto.Email);

        var existingCoach = await _coachRepository.GetByEmailAsync(dto.Email);
        if (existingCoach != null)
        {
            _logger.LogDebug("Coach already exists: {email}", dto.Email);
            return null; // sette opp custom exception? coach already exists. Returnerer nå bare BadRequesten fra controlleren.
        }

        var coach = _coachRegistartionMapper.MapToEntity(dto);

        coach.Id = CoachId.NewId; // Generate a new CoachId. Må ha med for at CoachID Guid skal fungere.
        coach.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        coach.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, coach.Salt);

        var res = await _coachRepository.RegisterCoachAsync(coach);

        return res != null ? _coachMapper.MapToDTO(res) : null;
    }
}