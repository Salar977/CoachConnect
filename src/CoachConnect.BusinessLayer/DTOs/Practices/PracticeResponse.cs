using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practice;

public record PracticeResponse(PracticeId PracticeId,
                               string? Location,
                               DateTime? PracticeDate,
                               DateTime? Created,
                               DateTime? Updated,
                               IEnumerable<PracticeAttendance>? PracticeAttendances);