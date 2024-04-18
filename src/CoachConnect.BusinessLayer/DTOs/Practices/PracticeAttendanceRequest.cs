using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceRequest(Guid PracticeId, Guid PlayerId);