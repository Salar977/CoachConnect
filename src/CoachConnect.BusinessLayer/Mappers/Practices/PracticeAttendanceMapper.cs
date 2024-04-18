using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeAttendanceMapper : IMapper<PracticeAttendance, PracticeAttendanceResponse>
{
    public PracticeAttendanceResponse MapToDTO(PracticeAttendance entity)
    {
        return new PracticeAttendanceResponse(entity.Id.practiceAttendanceId,
                                              entity.PracticeId.practiceId,
                                              entity.PlayerId.playerId,
                                              entity.Created.ToString("f"),
                                              entity.Updated.ToString("f"));
    }

    public PracticeAttendance MapToEntity(PracticeAttendanceResponse practiceAttendance)
    {
        throw new NotImplementedException();
    }
}