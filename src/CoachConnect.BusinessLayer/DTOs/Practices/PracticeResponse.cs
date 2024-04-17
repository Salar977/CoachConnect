using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practice;

public record PracticeResponse(Guid? PracticeId,
                               string? Location,
                               DateTime? PracticeDate,
                               DateTime? Created,
                               DateTime? Updated,
                               IEnumerable<PracticeAttendance>? PracticeAttendances);