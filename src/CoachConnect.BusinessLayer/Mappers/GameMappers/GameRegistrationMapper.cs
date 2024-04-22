using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.GameMappers;
public class GameRegistrationMapper : IMapper<Game, GameRegistrationDTO>
{
    public GameRegistrationDTO MapToDTO(Game entity)
    {
        return new GameRegistrationDTO(
            entity.Location,
            entity.HomeTeam,
            entity.AwayTeam,
            entity.GameTime
        );
    }

    public Game MapToEntity(GameRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Game()
        {
            Location = dto.Location,
            HomeTeam = dto.HomeTeam,
            AwayTeam = dto.AwayTeam,
            GameTime = dto.GameTime,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
