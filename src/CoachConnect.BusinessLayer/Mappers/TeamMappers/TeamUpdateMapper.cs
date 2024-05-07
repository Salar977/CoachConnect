using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Teams;
public class TeamUpdateMapper : IMapper<Team, TeamUpdate>
{
    public TeamUpdate MapToDTO(Team entity)
    {
        throw new NotImplementedException();
    }

    public Team MapToEntity(TeamUpdate dto)
    {
        var dtnow = DateTime.Now;
        return new Team
        {
            TeamCity = dto.TeamCity,
            TeamName = dto.TeamName,
            Updated = dtnow
        };
    }
}
