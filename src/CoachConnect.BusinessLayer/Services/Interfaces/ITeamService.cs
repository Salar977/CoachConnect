using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

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
