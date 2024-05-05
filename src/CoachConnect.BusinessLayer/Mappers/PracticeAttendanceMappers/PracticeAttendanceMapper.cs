using CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.PracticeAttendanceMappers;

public class PracticeAttendanceMapper : IMapper<PracticeAttendance, PracticeAttendanceResponse>
{
    public PracticeAttendanceResponse MapToDTO(PracticeAttendance entity)
    {
        return new PracticeAttendanceResponse(entity.Id.practiceAttendanceId,
                                              entity.PracticeId.practiceId,
                                              entity.PlayerId.playerId,
                                              entity.Player!.FirstName,
                                              entity.Player.LastName,
                                              entity.Created,
                                              entity.Updated);
    }

    public PracticeAttendance MapToEntity(PracticeAttendanceResponse practiceAttendance)
    {
        throw new NotImplementedException();
    }
}