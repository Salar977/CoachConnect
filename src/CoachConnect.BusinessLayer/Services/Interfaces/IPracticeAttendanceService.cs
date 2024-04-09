using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Services.Interfaces;

public interface IPracticeAttendanceService
{
    Task<PracticeAttendanceResponse?> RegisterPracticeAttendanceAsync(PracticeAttendanceRequest practiceAttendanceRequest);
    Task<PracticeAttendanceResponse?> DeleteByIdAsync(Guid id);
    Task<PracticeAttendanceResponse?> GetByIdAsync(Guid id);

    Task<IEnumerable<PracticeAttendanceResponse>> GetByPracticeAsync(Guid id);
}