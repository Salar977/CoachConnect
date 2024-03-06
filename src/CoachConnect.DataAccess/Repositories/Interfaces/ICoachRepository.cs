using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface ICoachRepository
{
    Task<ICollection<Coach>> GetAllAsync(QueryObject query);
    Task<Coach>? GetByIdAsync(CoachId id);
    Task<Coach>? GetByEmailAsync(string email);
    Task<Coach>? UpdateAsync(CoachId id, Coach coach);
    Task<Coach>? DeleteAsync(CoachId id);
    Task<Coach>? RegisterCoachAsync(Coach coach);
}
