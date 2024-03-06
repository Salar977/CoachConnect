using CoachConnect.BusinessLayer.DTOs;
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
    private readonly IMapper<Coach, CoachRegistrationDTO> _coachRegistartionMapper;
    private readonly ILogger<CoachService> _logger;

    public CoachService(ICoachRepository coachRepository,
                        IMapper<Coach, CoachDTO> coachMapper,
                        IMapper<Coach, CoachRegistrationDTO> coachRegistrationMapper,
                        ILogger<CoachService> logger)
    {
        _coachRepository = coachRepository;
        _coachMapper = coachMapper;
        _coachRegistartionMapper = coachRegistrationMapper;
        _logger = logger;
    }

    public async Task<ICollection<CoachDTO>> GetAllAsync(QueryObject query)
    {
        _logger.LogDebug("Getting all coaches");
        var res = await _coachRepository.GetAllAsync(query);
        return res.Select(coach => _coachMapper.MapToDTO(coach)).ToList();
    }

    public async Task<CoachDTO?> GetByIdAsync(CoachId id)
    {
        _logger.LogDebug("Getting coach by id: {id}", id);

        var res = await _coachRepository.GetByIdAsync(id);
        return res != null ? _coachMapper.MapToDTO(res) : null;
    }

    public async Task<CoachDTO?> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting coach by email: {email}", email);

        var res = await _coachRepository.GetByEmailAsync(email);
        return res != null ? _coachMapper.MapToDTO(res) : null;
    }     

    public async Task<CoachDTO?> UpdateAsync(CoachId id, CoachDTO dto)
    {
        _logger.LogDebug("Updating coach: {id}", id);

        // husk at coaches (el admin) kun skal kunne eoppdatere sin egen coach Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
        // kanksje noe som : throw new UnauthorizedAccessException($"Coach {loggedInUserId} has no access to delete coach {id}");

        var coach = _coachMapper.MapToEntity(dto);
        coach.Id = id;

        var res = await _coachRepository.UpdateAsync(id, coach);
        return res != null ? _coachMapper.MapToDTO(coach) : null;
    }

    public Task<CoachDTO?> DeleteAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public async Task<CoachDTO?> RegisterCoachAsync(CoachRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new coach: {dto}", dto);

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