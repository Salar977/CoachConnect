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
            entity.OpponentName,
            entity.GameTime,
            entity.Id
            );
    }

    public Game MapToEntity(GameDTO dto)
    {
        throw new NotImplementedException();
    }
}
