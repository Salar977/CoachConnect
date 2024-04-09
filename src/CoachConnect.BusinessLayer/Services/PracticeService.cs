using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class PracticeService : IPracticeService
{
    private readonly IPracticeRepository _practiceRepository;
    private readonly IGameRepository _gameRepository;

    private readonly IMapper<Practice, PracticeResponse> _practiceMapper;
    private readonly IMapper<Practice, PracticeRequest> _practiceRequestMapper;
    private readonly IMapper<Practice, PracticeUpdate> _practiceUpdateMapper;
    private readonly ILogger<PracticeService> _logger;

    public PracticeService(IPracticeRepository practiceRepository,
                           IGameRepository gameRepository,

                           IMapper<Practice, PracticeResponse> practiceMapper,
                           IMapper<Practice, PracticeRequest> practiceRequestMapper,
                           IMapper<Practice, PracticeUpdate> practiceUpdateMapper,
                           ILogger<PracticeService> logger)
    {
        _practiceRepository = practiceRepository;
        _gameRepository = gameRepository;

        _practiceMapper = practiceMapper;
        _practiceRequestMapper = practiceRequestMapper;
        _practiceUpdateMapper = practiceUpdateMapper;
        _logger = logger;
    }

    public async Task<PracticeResponse?> DeleteAsync(Guid practiceId)
    {
        _logger.LogDebug("Deleting Practice: {id}", practiceId);

        var res = await _practiceRepository.DeleteAsync(new PracticeId(practiceId));
        return res != null ? _practiceMapper.MapToDTO(res) : null;
    }

    public async Task<IEnumerable<PracticeResponse>> GetAllAsync(PracticeQuery practiceQuery)
    {
        _logger.LogDebug("Get all practices - Service");
        var res = await _practiceRepository.GetAllAsync(practiceQuery);
        return res.Select(_practiceMapper.MapToDTO).ToList();
    }

    public async Task<PracticeResponse?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Get practice by id: {id}", id);

        var res = await _practiceRepository.GetByIdAsync(new PracticeId(id));

        return res != null ? _practiceMapper.MapToDTO(res) : null;
    }

    public async Task<PracticeResponse?> RegisterPracticeAsync(PracticeRequest practice)
    {
        try
        {    

            DateTime startDate = practice.PracticeDate.Date;

            var practiceExists = _practiceRepository.GetByPracticeTimeAsync(startDate);

            if (practiceExists is null)
            {
                var practiceEntity = _practiceRequestMapper.MapToEntity(practice);
                practiceEntity.Id = PracticeId.NewId;

                var res = await _practiceRepository.RegisterPracticeAsync(practiceEntity);

                if (res is null) return null;

                _logger.LogInformation("Registered practice - Service");
                return _practiceMapper.MapToDTO(res);
            }
            return null;

        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong with practice registration, {@ex}", ex);
            return null;
        }
    }

    public async Task<PracticeResponse?> UpdateAsync(Guid id, PracticeUpdate practice)
    {
        try
        {
            var practiceId = new PracticeId(id);
            var practiceToUpdate = await _practiceRepository.GetByIdAsync(practiceId);
            if(practiceToUpdate is null)
            {
                _logger.LogWarning("Practice by id {id} does not exist", id);
            }
            var updatedPractice = _practiceUpdateMapper.MapToEntity(practice);
            updatedPractice.Id = practiceId;
            await _practiceRepository.UpdateAsync(practiceId, updatedPractice);

            var grabPractice = await _practiceRepository.GetByIdAsync(practiceId);

            if (grabPractice is null) return null;

            _logger.LogInformation("Practice was updated - Service");

            return _practiceMapper.MapToDTO(grabPractice);
        }
        catch(Exception ex)
        {
            _logger.LogError("{ex}", ex.Message);
            return null;
        }
    }
}