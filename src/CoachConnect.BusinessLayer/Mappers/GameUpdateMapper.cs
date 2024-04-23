using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers;
public class GameUpdateMapper : IMapper<Game, GameUpdateDTO>
{
    public GameUpdateDTO MapToDTO(Game entity)
    {
        return new GameUpdateDTO(
            entity.Location,
            entity.OpponentName,
            entity.GameTime
            );
    }

    public Game MapToEntity(GameUpdateDTO dto)
    {
        var dtnow = DateTime.Now;
        return new Game
        {
            Location = dto.Location,
            OpponentName = dto.OpponentName,
            GameTime = dto.GameTime
        };
    }
}
