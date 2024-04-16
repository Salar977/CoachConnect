using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceResponse(Guid PracticeAttendanceId,
                                         Guid PracticeId,
                                         Guid PlayerId,
                                         DateTime Created,
                                         DateTime Updated);