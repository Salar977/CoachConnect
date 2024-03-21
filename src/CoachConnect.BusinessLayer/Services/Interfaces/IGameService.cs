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
    Task<GameDTO?> GetByIdAsync(Guid id); // Hent en kamp ved hjelp av spillens ID
    Task<ICollection<GameDTO>> GetAllAsync(GameQuery gameQuery); // Hent alle kampene i systemet med paginering
    Task<GameDTO?> CreateAsync(GameDTO gameDTO); // Opprette en ny kamp
    Task<GameDTO?> UpdateAsync(Guid id, GameDTO gameDto); // Oppdater et eksisterende kamp
    Task<GameDTO?> DeleteAsync(Guid id); // Slett en kamp basert på ID
}
