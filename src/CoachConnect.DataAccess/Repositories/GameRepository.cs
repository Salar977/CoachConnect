using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly CoachConnectDbContext _dbContext; // Deklarerer et privat medlemsfelt som holder referansen til databasetilgangskonteksten.
        private readonly ILogger<GameRepository> _logger; // Deklarerer et privat medlemsfelt som holder referansen til en logger for denne klassen.

        public GameRepository(CoachConnectDbContext dbContext, ILogger<GameRepository> logger) // Konstruktør som tar en databasekontekst og en logger som argumenter og tilordner dem til de tilsvarende medlemsfeltene.
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Game> CreateAsync(Game game) // Metode som oppretter et nytt spill i databasen.
        {
            if (game == null) // Sjekker om det medfølgende game-objektet er null.
            {
                throw new ArgumentNullException(nameof(game), "Game object cannot be null"); // Kaster et unntak hvis game-objektet er null.
            }

            //game.Created = DateTime.UtcNow; // Setter opprettelsestidspunktet til den nåværende UTC-tiden.
            game.Updated = DateTime.UtcNow; // Setter oppdateringstidspunktet til den nåværende UTC-tiden.

            await _dbContext.Games.AddAsync(game); // Legger til det nye spillet i databasen asynkront.
            await _dbContext.SaveChangesAsync(); // Lagrer endringene i databasen asynkront.

            return game; // Returnerer det opprettede spillet.
        }

        public async Task<bool> DeleteAsync(GameId id) // Metode som sletter et spill fra databasen basert på ID.
        {
            var game = await _dbContext.Games.FindAsync(id); // Finner spillet med den angitte ID-en asynkront.
            if (game == null) // Sjekker om spillet ble funnet.
            {
                return false; // Returnerer false hvis spillet ikke ble funnet.
            }

            _dbContext.Games.Remove(game); // Fjerner spillet fra databasen.
            await _dbContext.SaveChangesAsync(); // Lagrer endringene i databasen asynkront.
            return true; // Returnerer true hvis spillet ble funnet og slettet.
        }

        public async Task<IEnumerable<Game>> GetAllAsync(int page, int pageSize) // Metode som henter alle spillene fra databasen med paginering.
        {
            return await _dbContext.Games // Returnerer en liste over spillene basert på paginering.
                .Skip((page - 1) * pageSize) // Hopper over de første (page - 1) sidene.
                .Take(pageSize) // Tar de neste 'pageSize' spillene.
                .ToListAsync(); // Konverterer resultatet til en liste asynkront.
        }

        public async Task<IEnumerable<Game>> GetByGameTimeAsync(DateTime gameTime) // Metode som henter spill basert på spilletidspunktet.
        {
            return await _dbContext.Games.Where(g => g.GameTime == gameTime).ToListAsync(); // Returnerer spillene som har angitt spilletidspunkt asynkront.
        }

        public async Task<Game?> GetByIdAsync(GameId id) // Metode som henter et spill basert på GameID.
        {
            return await _dbContext.Games.FindAsync(id); // Returnerer spillet med den angitte ID-en asynkront.
        }

        public async Task<IEnumerable<Game>> GetByLocationAsync(string location) // Metode som henter spill basert på lokasjon.
        {
            return await _dbContext.Games.Where(g => g.Location == location).ToListAsync(); // Returnerer spillene som har angitt lokasjon asynkront.
        }

        public async Task<IEnumerable<Game>> GetByOpponentNameAsync(string opponentName) // Metode som henter spill basert på motstanderens navn.
        {
            return await _dbContext.Games.Where(g => g.OpponentName == opponentName).ToListAsync(); // Returnerer spillene som har angitt motstanderens navn asynkront.
        }

        public async Task<Game> UpdateAsync(GameId id, Game game) // Metode som oppdaterer et eksisterende spill.
        {
            var existingGame = await _dbContext.Games.FindAsync(id); // Finner det eksisterende spillet basert på ID-en asynkront.
            if (existingGame == null) // Sjekker om spillet ble funnet.
            {
                throw new KeyNotFoundException($"Game with ID '{id}' not found"); // Kaster et unntak hvis spillet ikke ble funnet.
            }

            // Oppdaterer egenskapene til det eksisterende spillet med egenskapene til det nye spillet
            existingGame.Location = game.Location; // Oppdaterer lokasjonen til det eksisterende spillet.
            existingGame.OpponentName = game.OpponentName; // Oppdaterer motstanderens navn til det eksisterende spillet.
            existingGame.GameTime = game.GameTime; // Oppdaterer spilletidspunktet til det eksisterende spillet.
            existingGame.Updated = DateTime.UtcNow; // Oppdaterer oppdateringstidspunktet til det eksisterende spillet til nåværende UTC-tid.

            await _dbContext.SaveChangesAsync(); // Lagrer endringene i databasen asynkront.
            return existingGame; // Returnerer det oppdaterte spillet.
        }
    }
}