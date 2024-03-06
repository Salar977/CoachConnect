using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class CoachService : ICoachService
{
    private readonly ILogger<CoachService> _logger;
    private readonly IMapper<Coach, CoachDTO> _coachMapper;
    private readonly IMapper<Coach, CoachRegistrationDTO> _coachRegistartionMapper;

    public CoachService(IMapper<Coach, CoachDTO> coachMapper,
                        IMapper<Coach, CoachRegistrationDTO> coachRegistrationMapper,
                        ILogger<CoachService> logger)
    {
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