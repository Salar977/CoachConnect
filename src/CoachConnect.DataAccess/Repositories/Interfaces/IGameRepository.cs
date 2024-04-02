using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IGameRepository
{
    Task<Game?> GetByIdAsync(GameId id); // Hent en kamp ved hjelp av spillens ID

    Task<ICollection<Game>> GetAllAsync(GameQuery gameQuery); // Hent alle kampene i systemet med paginering

    //Task<ICollection<Game>> GetByOpponentNameAsync(string opponentName); // Hent kamper basert på motstanderens navn

    //Task<ICollection<Game>> GetByLocationAsync(string location); // Hent kamp basert på plasseringen

    Task<Game?> GetByGameTimeAsync(DateTime gameTime); // Hent kamp basert på spilltidspunktet

    Task<Game?> CreateAsync(Game game); // Opprette en ny kamp

    Task<Game?> UpdateAsync(GameId id, Game game); // Oppdater et eksisterende kamp

    Task<Game?> DeleteAsync(GameId id); // Slett en kamp basert på ID
}
