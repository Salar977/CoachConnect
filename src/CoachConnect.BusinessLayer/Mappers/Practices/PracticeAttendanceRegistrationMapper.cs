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
        return new PracticeAttendance
        {
            PracticeId = new PracticeId(dto.PracticeId),
            PlayerId = new PlayerId(dto.PlayerId),
            Created = DateTime.Now,
            Updated = DateTime.MinValue
        };
    }
}