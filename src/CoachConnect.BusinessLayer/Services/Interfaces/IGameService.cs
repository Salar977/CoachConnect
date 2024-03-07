using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IGameService
{
    Task<GameDTO?> GetByIdAsync(GameId id); // Hent en kamp ved hjelp av spillens ID

    Task<ICollection<GameDTO>> GetAllAsync(int page, int pageSize); // Hent alle kampene i systemet med paginering

    Task<ICollection<GameDTO>> GetByOpponentNameAsync(string opponentName); // Hent kamper basert på motstanderens navn

    Task<ICollection<GameDTO>> GetByLocationAsync(string location); // Hent kamp basert på plasseringen

    Task<ICollection<GameDTO>> GetByGameTimeAsync(DateTime gameTime); // Hent kamp basert på spilltidspunktet

    Task<GameDTO?> CreateAsync(GameDTO gameDTO); // Opprette en ny kamp

    Task<GameDTO?> UpdateAsync(GameId id, GameDTO gameDto); // Oppdater et eksisterende kamp

    Task<GameDTO?> DeleteAsync(GameId id); // Slett en kamp basert på ID

}
