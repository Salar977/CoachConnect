using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IPracticeService
{
    Task<IEnumerable<PracticeResponse>> GetAllAsync(PracticeQuery practiceQuery);
    Task<PracticeResponse?> GetByIdAsync(Guid id);
    Task<PracticeResponse?> RegisterPracticeAsync(PracticeRequest practice);
    Task<PracticeResponse?> UpdateAsync(Guid id, PracticeUpdate practice);
    Task<PracticeResponse?> DeleteAsync(Guid practiceId);
}
