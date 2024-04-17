using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;


namespace CoachConnect.DataAccess.Repositories.Interfaces;

public interface IPracticeAttendanceRepository
{
    //Task<IEnumerable<PracticeAttendance>> GetAllAsync(PracticeAttendanceQuery practiceAttendanceQuery);
    Task<PracticeAttendance?> RegisterAsync(PracticeAttendance practiceAttendance);

    Task<PracticeAttendance?> DeleteByIdAsync(PracticeAttendanceId id);
    Task<PracticeAttendance?> GetByIdAsync(PracticeAttendanceId id);
    Task<PracticeAttendance?> GetByPracticeIdAndPlayerIdAsync(PracticeId practiceId, PlayerId playerId);

    Task<IEnumerable<PracticeAttendance>> GetByPracticeIdAsync(PracticeId id);
}