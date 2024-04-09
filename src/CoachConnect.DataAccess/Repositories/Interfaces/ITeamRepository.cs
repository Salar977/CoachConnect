using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface ITeamRepository
{
    Task<ICollection<Team>> GetAllAsync(TeamQuery teamquery);
    Task<Team?> GetByIdAsync(TeamId id);
    Task<Team?> GetByCoachIdAsync(CoachId coachid);
    Task<Team?> UpdateAsync(TeamId id, Team team);
    Task<Team?> DeleteAsync(TeamId id);
    Task<Team?> RegisterTeamAsync(Team team);
}
