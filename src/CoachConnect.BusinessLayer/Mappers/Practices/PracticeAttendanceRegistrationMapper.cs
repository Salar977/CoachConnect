using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeAttendanceRegistrationMapper : IMapper<PracticeAttendance, PracticeAttendanceRequest>
{
    public PracticeAttendanceRequest MapToDTO(PracticeAttendance entity)
    {
        throw new NotImplementedException();
    }

    public PracticeAttendance MapToEntity(PracticeAttendanceRequest dto)
    {
        var now = DateTime.Now;
        return new PracticeAttendance
        {
            PracticeId = dto.PracticeId,
            PlayerId = dto.PlayerId,
            Created = now,
            Updated = now
        };
    }
}