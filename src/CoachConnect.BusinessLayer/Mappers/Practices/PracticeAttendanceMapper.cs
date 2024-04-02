using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeAttendanceMapper : IMapper<PracticeAttendance, PracticeAttendanceResponse>
{
    public PracticeAttendanceResponse MapToDTO(PracticeAttendance entity)
    {
        return new PracticeAttendanceResponse(entity.Id,
                                              entity.PracticeId,
                                              entity.PlayerId,
                                              entity.Created,
                                              entity.Updated);
    }

    public PracticeAttendance MapToEntity(PracticeAttendanceResponse practiceAttendance)
    {
        return new PracticeAttendance
        {
            Id = practiceAttendance.PracticeAttendanceId,
            PracticeId = practiceAttendance.PracticeId,
            PlayerId = practiceAttendance.PlayerId,
            Created = practiceAttendance.Created,
            Updated = practiceAttendance.Updated
        };
    }
}