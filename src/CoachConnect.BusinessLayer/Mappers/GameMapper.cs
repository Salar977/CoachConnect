using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class GameMapper : IMapper<Game, GameDTO>
{
    public GameDTO MapToDTO(Game entity)
    {
        return new GameDTO(
            entity.Id,
            entity.Location,
            entity.OpponentName,
            entity.GameTime,
            entity.Created,
            entity.Updated
            );
    }

    public Game MapToEntity(GameDTO dto)
    {
        return new Game
        {
            Id = dto.Id,
            Location = dto.Location,
            OpponentName = dto.OpponentName,
            GameTime = dto.GameTime,
            Created = dto.Created,
            Updated = dto.Updated
        };
    }
}
