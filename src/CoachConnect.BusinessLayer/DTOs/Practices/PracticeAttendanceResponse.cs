namespace CoachConnect.BusinessLayer.DTOs.Practices;

public record PracticeAttendanceResponse(Guid PracticeAttendanceId,
                                         Guid PracticeId,
                                         Guid PlayerId,
                                         string FirstName,
                                         string LastName,
                                         DateTime Created,
                                         DateTime Updated);