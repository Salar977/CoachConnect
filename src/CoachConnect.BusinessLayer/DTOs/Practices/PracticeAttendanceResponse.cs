using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceResponse(PracticeAttendanceId PracticeAttendanceId,
                                         PracticeId PracticeId,
                                         PlayerId PlayerId,
                                         DateTime Created,
                                         DateTime Updated);