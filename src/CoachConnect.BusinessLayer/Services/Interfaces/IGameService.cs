using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IGameService
{
    Task<ICollection<GameDTO>> GetAllAsync(GameQuery gameQuery);
    Task<GameDTO?> GetByIdAsync(Guid id);
    Task<GameUpdateDTO?> UpdateAsync(bool isAdmin, string idFromToken, Guid id, GameUpdateDTO gameUpdateDto);
    Task<GameRegistrationDTO?> CreateAsync(bool isAdmin, string idFromToken, GameRegistrationDTO gameRegistrationDTO); 
    Task<GameDTO?> DeleteAsync(bool isAdmin, string idFromToken, Guid id); 
}
