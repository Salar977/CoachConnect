using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface ITeamRepository
{
    Task<ICollection<Team>> GetAllAsync(CoachQuery query);
    Task<Team?> GetByIdAsync(TeamId id);
    Task<Team?> GetByCoachIdAsync(CoachId coachid);
    Task<Team?> GetByTeamAsync(string team);
    Task<Team?> UpdateAsync(TeamId id, Team team);
    Task<Team?> DeleteAsync(TeamId id);
    Task<Team?> RegisterCoachAsync(Team coach);
}
