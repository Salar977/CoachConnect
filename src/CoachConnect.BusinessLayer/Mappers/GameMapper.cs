using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class GameMapper : IMapper<Game, GameDTO>
{
    public GameDTO MapToDTO(Game entity)
    {
        return new GameDTO(
            entity.Location,
            entity.OpponentName,
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
            OpponentName = dto.OpponentName,
            GameTime = dto.GameTime,
            Created = dtnow,
            Updated = dtnow,
        };
    }
}
