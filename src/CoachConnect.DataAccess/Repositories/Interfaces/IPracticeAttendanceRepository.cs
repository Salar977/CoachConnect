using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;


namespace CoachConnect.DataAccess.Repositories.Interfaces;

public interface IPracticeAttendanceRepository
{
    //Task<IEnumerable<PracticeAttendance>> GetAllAsync(PracticeAttendanceQuery practiceAttendanceQuery);
    Task<PracticeAttendance?> RegisterAsync(PracticeAttendance practiceAttendance);

    Task<PracticeAttendance?> DeleteByIdAsync(PracticeAttendanceId id);
    Task<PracticeAttendance?> GetByIdAsync(PracticeAttendanceId id);

    Task<IEnumerable<PracticeAttendance>> GetByPracticeAsync(PracticeId id);
}