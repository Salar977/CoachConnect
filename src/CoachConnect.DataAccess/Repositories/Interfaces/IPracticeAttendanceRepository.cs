using CoachConnect.DataAccess.Entities;

namespace CoachConnect.DataAccess.Repositories.Interfaces;

public interface IPracticeAttendanceRepository
{
    Task<PracticeAttendance?> RegisterPracticeAttendanceAsync(PracticeAttendance practiceAttendance);
    Task<PracticeAttendance?> DeleteByIdAsync(PracticeAttendanceId id);
    Task<PracticeAttendance?> GetByIdAsync(PracticeAttendanceId id);
}