using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.Teams;
public class TeamRegistrationMapper : IMapper<Team, TeamRequest>
{
    public TeamRequest MapToDTO(Team entity)
    {
        throw new NotImplementedException();
    }

    public Team MapToEntity(TeamRequest dto)
    { 
        var dtnow = DateTime.Now;
        return new Team
        {
            CoachId = new CoachId(dto.CoachId),
            TeamCity = dto.TeamCity,
            TeamName = dto.TeamName,
            Created = dtnow,
            Updated = DateTime.MinValue
        };
    }
}
