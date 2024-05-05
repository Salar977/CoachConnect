using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface ITeamService
{
    Task<TeamResponse?> GetByIdAsync(TeamId id);
    Task<ICollection<TeamResponse?>> GetTeamsByCoachId(CoachId coachid);

    Task<ICollection<TeamResponse>> GetAllAsync(TeamQuery teamQuery); 

    Task<TeamResponse?> CreateAsync(TeamRequest teamReq); 

    Task<TeamResponse?> UpdateAsync(TeamId id, TeamUpdate teamupdate); 

    Task<TeamResponse?> DeleteAsync(TeamId id); 
}
