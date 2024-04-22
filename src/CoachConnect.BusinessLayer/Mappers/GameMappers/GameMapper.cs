using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.GameMappers;

public class GameMapper : IMapper<Game, GameDTO>
{
    public GameDTO MapToDTO(Game entity)
    {
        return new GameDTO(
            entity.Location,
            entity.HomeTeam,
            entity.AwayTeam,
            entity.GameTime,
            entity.Id
            );
    }

    public Game MapToEntity(GameDTO dto)
    {
        var dtnow = DateTime.Now;
        return new Game
        {
            Location = dto.Location,
            HomeTeam = dto.HomeTeam,
            AwayTeam = dto.AwayTeam,
            GameTime = dto.GameTime,
            Created = dtnow,
            Updated = dtnow
        };
    }
}
