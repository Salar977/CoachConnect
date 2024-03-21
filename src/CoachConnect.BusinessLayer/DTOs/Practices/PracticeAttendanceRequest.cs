using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceRequest(PracticeId PracticeId, PlayerId PlayerId);