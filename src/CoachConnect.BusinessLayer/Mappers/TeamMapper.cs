using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers;
public class TeamMapper : IMapper<Team, TeamDTO>
{
    public TeamDTO MapToDTO(Team entity)
    {
        return new TeamDTO(
            entity.TeamCity,
            entity.TeamName,
            entity.Updated,
            entity.Created,
            entity.CoachId,
            entity.Id
            );
    }

    public Team MapToEntity(TeamDTO dto)
    {
        var dtnow = DateTime.Now;
        return new Team
        {
            TeamCity = dto.TeamCity,
            TeamName = dto.TeamName,
            Created = dtnow,
            Updated = dtnow,
        };
    }
}
