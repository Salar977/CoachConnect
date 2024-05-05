using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.PlayerMappers;
public class PlayerRegistrationMapper : IMapper<Player, PlayerRequest>
{
    public PlayerRequest MapToDTO(Player entity)
    {
        throw new NotImplementedException();
    }

    public Player MapToEntity(PlayerRequest dto)
    {
        var now = DateTime.Now;
        return new Player
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserId = dto.UserId,
            TeamId = dto.TeamId,
            Created = now,
            Updated = DateTime.MinValue
        };
    }
}
//new PlayerId(dto.PlayerId)