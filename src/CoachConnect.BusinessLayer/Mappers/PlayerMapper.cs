using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers;
public class PlayerMapper : IMapper<Player, PlayerDTO>
{
    public PlayerDTO MapToDTO(Player entity)
    {
        return new PlayerDTO(
            entity.FirstName,
            entity.LastName,
            entity.Created,
            entity.Updated,
            entity.UserId,
            entity.TeamId,
            entity.Id
            );
    }

    public Player MapToEntity(PlayerDTO dto)
    {
        var dtnow = DateTime.Now;
        return new Player
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserId = dto.UserId,
            TeamId = dto.TeamId,
            Created = dto.Created,
            Updated = dtnow
        };
    }
}
