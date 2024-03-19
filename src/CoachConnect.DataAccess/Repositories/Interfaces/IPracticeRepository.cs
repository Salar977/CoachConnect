using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

/*
 * Author: Salar Amir
 */

namespace CoachConnect.DataAccess.Repositories.Interfaces;

public interface IPracticeRepository
{
    Task<IEnumerable<Practice>> GetAllAsync(PracticeQuery practiceQuery);
    Task<Practice?> GetByIdAsync(PracticeId id);
    Task<Practice?> RegisterPracticeAsync(Practice practice);
    Task<Practice?> UpdateAsync(PracticeId id, Practice practice);
    Task<Practice?> DeleteAsync(PracticeId practiceId);
}