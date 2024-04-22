using CoachConnect.BusinessLayer.DTOs.GameAttendances;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.GameAttendanceMapper;
public class GameAttendanceRegistrationMapper : IMapper<GameAttendance, GameAttendanceRegistrationDTO>
{
    public GameAttendanceRegistrationDTO MapToDTO(GameAttendance entity)
    {
        return new GameAttendanceRegistrationDTO(
        entity.GameId,
        entity.PlayerId
        );
    }

    public GameAttendance MapToEntity(GameAttendanceRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new GameAttendance()
        {
            GameId = dto.GameId,
            PlayerId = dto.PlayerId,
            Created = dtNow,
            Updated = dtNow,
        };
    }
}
