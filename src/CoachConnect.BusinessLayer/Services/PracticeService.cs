using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoachConnect.BusinessLayer.Services;

public class PracticeService : IPracticeService
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly IMapper<Practice, PracticeResponse> _practiceMapper;
    private readonly ILogger<PracticeService> _logger;

    public PracticeService(IPracticeRepository practiceRepository,
                           IMapper<Practice, PracticeResponse> practiceMapper,
                           ILogger<PracticeService> logger)
    {
        _practiceRepository = practiceRepository;
        _practiceMapper = practiceMapper;
        _logger = logger;
    }

    public async Task<PracticeResponse?> DeleteAsync(PracticeId practiceId)
    {
        _logger.LogDebug("Deleting Practice: {id}", practiceId);

        var res = await _practiceRepository.DeleteAsync(practiceId);
        return res != null ? _practiceMapper.MapToDTO(res) : null;
    }

    public async Task<IEnumerable<PracticeResponse>> GetAllAsync(PracticeQuery practiceQuery)
    {
        _logger.LogDebug("Get all practices - PracticeService");
        var res = await _practiceRepository.GetAllAsync(practiceQuery);
        return res.Select(_practiceMapper.MapToDTO).ToList();
    }

    public async Task<PracticeResponse?> GetByIdAsync(PracticeId id)
    {
        _logger.LogDebug("Get practice by id: {id}", id);

        var res = await _practiceRepository.GetByIdAsync(id);
        return res != null ? _practiceMapper.MapToDTO(res) : null;
    }

    public Task<PracticeResponse?> RegisterPracticeAsync(PracticeRequest practice)
    {
        throw new NotImplementedException();
    }

    public Task<PracticeResponse?> UpdateAsync(PracticeId id, PracticeUpdate practice)
    {
        throw new NotImplementedException();
    }
}