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
    Task<Game> GetByIdAsync(GameId id); // Hent en kamp ved hjelp av spillens ID

    Task<IEnumerable<Game>> GetAllAsync(int page, int pageSize); // Hent alle kampene i systemet med paginering

    Task<IEnumerable<Game>> GetByOpponentNameAsync(string opponentName); // Hent kamper basert på motstanderens navn

    Task<IEnumerable<Game>> GetByLocationAsync(string location); // Hent kamp basert på plasseringen

    Task<IEnumerable<Game>> GetByGameTimeAsync(DateTime gameTime); // Hent kamp basert på spilltidspunktet

    Task<Game> CreateAsync(Game game); // Opprette en ny kamp

    Task<Game> UpdateAsync(GameId id, Game game); // Oppdater et eksisterende kamp

    Task<bool> DeleteAsync(GameId id); // Slett en kamp basert på ID


    // Andre spesifikke metoder basert på forretningslogikken din...
}
