using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.BusinessLayer.Services;

public class CoachService : ICoachService
{
    public Task<ICollection<CoachDTO>> GetAllAsync(QueryObject query)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> GetByIdAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }     

    public Task<CoachDTO> UpdateAsync(CoachId id, CoachDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> DeleteAsync(CoachId id)
    {
        throw new NotImplementedException();
    }

    public Task<CoachDTO> RegisterCoach(CoachRegistrationDTO dto)
    {
        throw new NotImplementedException();
    }
}