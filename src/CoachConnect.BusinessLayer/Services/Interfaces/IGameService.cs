using CoachConnect.BusinessLayer.DTOs.Games;
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
    Task<GameDTO?> GetByIdAsync(Guid id); 
    Task<ICollection<GameDTO>> GetAllAsync(GameQuery gameQuery); 
    Task<GameRegistrationDTO?> CreateAsync(bool isAdmin, string idFromToken, GameRegistrationDTO gameRegistrationDTO); 
    Task<GameUpdateDTO?> UpdateAsync(Guid id, GameUpdateDTO gameUpdateDto); 
    Task<GameDTO?> DeleteAsync(Guid id); 
}
