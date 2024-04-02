using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class PracticeAttendanceService : IPracticeAttendanceService
{
    private readonly IPracticeAttendanceRepository _practiceAttendance;
    private readonly IPracticeRepository _practiceRepository;
    private readonly IMapper<PracticeAttendance, PracticeAttendanceResponse> _mapper;
    private readonly IMapper<PracticeAttendance, PracticeAttendanceRequest> _requestMapper;
    private readonly ILogger<PracticeAttendanceService> _logger;

    public PracticeAttendanceService(IPracticeAttendanceRepository practiceAttendance,
                                     IPracticeRepository practiceRepository,
                                     IMapper<PracticeAttendance, PracticeAttendanceResponse> mapper,
                                     IMapper<PracticeAttendance, PracticeAttendanceRequest> requestMapper,
                                     ILogger<PracticeAttendanceService> logger)
    {
        _practiceAttendance = practiceAttendance;
        _practiceRepository = practiceRepository;
        _mapper = mapper;
        _requestMapper = requestMapper;
        _logger = logger;
    }

    public async Task<PracticeAttendanceResponse?> DeleteByIdAsync(Guid id)
    {
        var practiceId = new PracticeAttendanceId(id);
        var res = await _practiceAttendance.DeleteByIdAsync(practiceId);

        if(res is null)
        {
            _logger.LogError("Cannot delete Practice Attendance");
            return null;
        }

        return _mapper.MapToDTO(res);
    }

    public async Task<PracticeAttendanceResponse?> GetByIdAsync(Guid id)
    {
        var res = await _practiceAttendance.GetByIdAsync(new PracticeAttendanceId(id));


        if (res is null)
        {
            _logger.LogError("Cannot find Practice Attendance");
            return null;
        }

        return _mapper.MapToDTO(res);
    }

    public async Task<PracticeAttendanceResponse?> RegisterPracticeAttendanceAsync(PracticeAttendanceRequest practiceAttendanceRequest)
    {
        try
        {
            var newAttendance = _requestMapper.MapToEntity(practiceAttendanceRequest);
            newAttendance.Id = PracticeAttendanceId.NewId;
            var addedAttendance = await _practiceAttendance.RegisterAsync(newAttendance);

            if(addedAttendance is null) return null;

            return _mapper.MapToDTO(addedAttendance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return null;
        }
    }
}