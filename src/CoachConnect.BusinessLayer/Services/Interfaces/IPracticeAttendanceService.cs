using CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.BusinessLayer.Services.Interfaces;

public interface IPracticeAttendanceService
{
    Task<IEnumerable<PracticeAttendanceResponse>> GetAllAsync(PracticeAttendanceQuery attendanceQuery);
    Task<IEnumerable<PracticeAttendanceResponse>> GetByPracticeAsync(Guid id);

    Task<PracticeAttendanceResponse?> RegisterPracticeAttendanceAsync(PracticeAttendanceRequest practiceAttendanceRequest);
    Task<PracticeAttendanceResponse?> DeleteByIdAsync(Guid id);
    Task<PracticeAttendanceResponse?> GetByIdAsync(Guid id);
}