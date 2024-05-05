using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.Teams;
public class TeamMapper : IMapper<Team, TeamResponse>
{
    public TeamResponse MapToDTO(Team entity)
    {
        return new TeamResponse(
            entity.TeamCity,
            entity.TeamName,
            entity.Updated,
            entity.Created,
            entity.CoachId.coachId,
            entity.Id.teamId
            );
    }

    public Team MapToEntity(TeamResponse dto)
    {
        throw new NotImplementedException();
    }
}
