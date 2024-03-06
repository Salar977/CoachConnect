using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class CoachService : ICoachService
{
    private readonly ILogger<CoachService> _logger;

    public CoachService(ILogger<CoachService> logger)
    {
        _logger = logger;
    }

    public async Task<ICollection<CoachDTO>> GetAllAsync(QueryObject query)
    {
        _logger.LogDebug("Getting all coaches");
        var res = await _coachRepository.GetAllAsync(query);
        return res.Select(coach => _coachMapper.MapToDTO(coach)).ToList();
    }

    public Task<CoachDTO> GetByIdAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }     

    public Task<CoachDTO> UpdateAsync(CoachId id, CoachDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> DeleteAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> RegisterCoach(CoachRegistrationDTO dto)
    {
        throw new NotImplementedException();
    }
}