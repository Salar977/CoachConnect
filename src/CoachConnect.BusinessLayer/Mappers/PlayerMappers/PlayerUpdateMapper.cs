using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.PlayerMappers;
public class PlayerUpdateMapper : IMapper<Player, PlayerUpdate>
{
    public PlayerUpdate MapToDTO(Player entity)
    {
        throw new NotImplementedException();
    }

    public Player MapToEntity(PlayerUpdate dto)
    {
        var now = DateTime.Now;
        return new Player
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Updated = now
        };
    }
}
