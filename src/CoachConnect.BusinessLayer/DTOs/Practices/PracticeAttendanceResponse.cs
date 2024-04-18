using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceResponse(Guid PracticeAttendanceId,
                                         PracticeId PracticeId,
                                         PlayerId PlayerId,
                                         DateTime Created,
                                         DateTime Updated);