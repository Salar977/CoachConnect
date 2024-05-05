using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.PlayerMappers;
public class PlayerMapper : IMapper<Player, PlayerResponse>
{
    public PlayerResponse MapToDTO(Player entity)
    {
        return new PlayerResponse(
            entity.FirstName,
            entity.LastName,
            entity.TotalGames,
            entity.TotalPractices,
            entity.Created,
            entity.Updated,
            entity.UserId.userId,
            entity.TeamId.teamId,
            entity.Id.playerId
            );
    }

    public Player MapToEntity(PlayerResponse dto)
    {
        throw new NotImplementedException();
    }
}
