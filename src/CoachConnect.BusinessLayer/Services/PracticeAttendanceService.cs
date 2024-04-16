using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class PracticeAttendanceService : IPracticeAttendanceService
{
    private readonly IPracticeAttendanceRepository _attendanceRepository;
    private readonly IPracticeRepository _practiceRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper<PracticeAttendance, PracticeAttendanceResponse> _mapper;
    private readonly IMapper<PracticeAttendance, PracticeAttendanceRequest> _requestMapper;
    private readonly ILogger<PracticeAttendanceService> _logger;

    public PracticeAttendanceService(IPracticeAttendanceRepository attendanceRepository,
                                     IPracticeRepository practiceRepository,
                                     IPlayerRepository playerRepository,
                                     IMapper<PracticeAttendance, PracticeAttendanceResponse> mapper,
                                     IMapper<PracticeAttendance, PracticeAttendanceRequest> requestMapper,
                                     ILogger<PracticeAttendanceService> logger)
    {
        _attendanceRepository = attendanceRepository;
        _practiceRepository = practiceRepository;
        _playerRepository = playerRepository;
        _mapper = mapper;
        _requestMapper = requestMapper;
        _logger = logger;
    }

    public async Task<PracticeAttendanceResponse?> DeleteByIdAsync(Guid id)
    {
        var practiceId = new PracticeAttendanceId(id);
        var res = await _attendanceRepository.DeleteByIdAsync(practiceId);

        if(res is null)
        {
            _logger.LogError("Cannot delete Practice Attendance");
            return null;
        }

        var player = await _playerRepository.GetByIdAsync(res.PlayerId);

        if (player is not null) { player.TotalPractices--; }

        return _mapper.MapToDTO(res);
    }

    public async Task<PracticeAttendanceResponse?> GetByIdAsync(Guid id)
    {
        var res = await _attendanceRepository.GetByIdAsync(new PracticeAttendanceId(id));


        if (res is null)
        {
            _logger.LogError("Cannot find Practice Attendance");
            return null;
        }

        return _mapper.MapToDTO(res);
    }

    public async Task<IEnumerable<PracticeAttendanceResponse>> GetByPracticeAsync(Guid id)
    {
        _logger.LogInformation("Get all attendances from practice - Service");
        var res = await _attendanceRepository.GetByPracticeIdAsync(new PracticeId(id));

        return res.Select(_mapper.MapToDTO).ToList();
    }

    public async Task<PracticeAttendanceResponse?> RegisterPracticeAttendanceAsync(PracticeAttendanceRequest practiceAttendanceRequest)
    {
        try
        {
            var newAttendance = _requestMapper.MapToEntity(practiceAttendanceRequest);
            newAttendance.Id = PracticeAttendanceId.NewId;

            // see if practice exists
            var practice = await _practiceRepository.GetByIdAsync(practiceAttendanceRequest.PracticeId);
            if (practice is null)
            {
                _logger.LogError("Practice with this id does not exist");
                return null;
            }

            // see if practice attendance exists in that practice
            var attendanceExist = _attendanceRepository.GetByPracticeIdAndPlayerIdAsync(newAttendance.PracticeId, newAttendance.PlayerId);
            if (attendanceExist is null)
            {
                _logger.LogError("Practice attendance already exists");
                return null;
            }

            var player = await _playerRepository.GetByIdAsync(practiceAttendanceRequest.PlayerId);

            if(player is not null) { player.TotalPractices++; }

            // add attendance to practice
            var addedAttendance = await _attendanceRepository.RegisterAsync(newAttendance);

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