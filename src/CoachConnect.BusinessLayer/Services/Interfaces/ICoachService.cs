using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
internal interface ICoachService
{
    Task<ICollection<CoachDTO>> GetAllAsync(QueryObject query);
    Task<CoachDTO> GetByIdAsync(CoachId id);
    Task<CoachDTO> GetByEmailAsync(string email);
    Task<CoachDTO> UpdateAsync(CoachId id, CoachDTO dto);
    Task<CoachDTO> DeleteAsync(CoachId id);
    Task<CoachDTO> RegisterCoach(CoachRegistrationDTO dto);
}