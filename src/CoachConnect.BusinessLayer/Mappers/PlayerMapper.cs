using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;
public class PlayerMapper : IMapper<Player, PlayerDTO>
{
    
    public PlayerDTO MapToDTO(Player entity)
    {
        return new PlayerDTO(
            entity.Id,
            entity.UserId,
            entity.TeamId,
            entity.FirstName,
            entity.LastName,
            entity.Created,
            entity.Updated
            );
    }
    
    public Player MapToEntity(PlayerDTO dto)
    {
        var dtnow = DateTime.Now;
        return new Player
        {
            
            UserId = dto.UserId,
            TeamId = dto.TeamId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Created = dtnow,
            Updated = dtnow
        };
    }
}
