using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers;
public class GameRegistrationMapper : IMapper<Game, GameRegistrationDTO>
{
    public GameRegistrationDTO MapToDTO(Game entity)
    {
        return new GameRegistrationDTO(
            entity.Location,
            entity.OpponentName,
            entity.GameTime            
        );
    }

    public Game MapToEntity(GameRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Game()
        {
            Location = dto.Location,
            OpponentName = dto.OpponentName,
            GameTime = dto.GameTime,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
