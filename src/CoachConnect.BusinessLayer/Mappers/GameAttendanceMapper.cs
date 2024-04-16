using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers;
public class GameAttendanceMapper : IMapper<GameAttendance, GameAttendanceDTO>
{
    public GameAttendanceDTO MapToDTO(GameAttendance entity)
    {
        return new GameAttendanceDTO(
            entity.Id,
            entity.GameId,
            entity.PlayerId
            );
    }

    public GameAttendance MapToEntity(GameAttendanceDTO dto)
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
