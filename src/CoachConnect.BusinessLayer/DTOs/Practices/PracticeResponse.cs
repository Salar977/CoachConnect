using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Practice;

public record PracticeResponse(Guid PracticeId,
                               string Location,
                               string PracticeDate,
                               string Created,
                               string? Updated);
                               //IEnumerable<PracticeAttendance>? PracticeAttendances);