namespace CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;

public record PracticeAttendanceResponse(Guid PracticeAttendanceId,
                                         Guid PracticeId,
                                         Guid PlayerId,
                                         string FirstName,
                                         string LastName,
                                         DateTime Created,
                                         DateTime Updated);