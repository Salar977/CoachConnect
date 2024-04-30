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
    Task<TeamDTO?> GetByIdAsync(TeamId id);
    Task<TeamDTO?> GetTeamsByCoachId(CoachId coachid);

    Task<ICollection<TeamDTO>> GetAllAsync(TeamQuery teamQuery); 

    Task<TeamDTO?> CreateAsync(TeamDTO teamDTO); 

    Task<TeamDTO?> UpdateAsync(TeamId id, TeamDTO teamDto); 

    Task<TeamDTO?> DeleteAsync(TeamId id); 
}
