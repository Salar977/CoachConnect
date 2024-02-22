using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services;
public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper<Game, GameDTO> _gameMapper;
    private readonly IMapper<User, UserRegistrationDTO> _userRegistrationMapper;
    private readonly ILogger<GameService> _logger;

    public GameService(IGameRepository gameRepository,
                       IMapper<Game, GameDTO> gameMapper,
                       IMapper<User, UserRegistrationDTO> userRegistrationMapper,
                       ILogger<GameService> logger)
    {
        _gameRepository = gameRepository;
        _gameMapper = gameMapper;
        _userRegistrationMapper = userRegistrationMapper;
        _logger = logger;
    }

    public async Task<Game> CreateAsync(Game game)
    {
        _logger.LogDebug("Creating new game");

        // Sett opprettelses- og oppdateringstidspunktet til nåværende tidspunkt
        //game.Created = DateTime.UtcNow;
        game.Updated = DateTime.UtcNow;

        // Bruk repository til å legge til det nye spillet i databasen
        var createdGame = await _gameRepository.CreateAsync(game);

        // Konverter det opprettede spillet til DTO og returner det
        return createdGame;
    }

    public Task CreateAsync(GameDTO gameDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(GameId id)
    {
        _logger.LogDebug("Deleting game");

        // Bruk repository til å slette spillet basert på ID
        var deleted = await _gameRepository.DeleteAsync(id);

        // Returner true hvis spillet ble slettet, ellers false
        return deleted;
    }

    public async Task<IEnumerable<Game>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting all games");

        // Bruk repository til å hente alle spillene med paginering
        var games = await _gameRepository.GetAllAsync(page, pageSize);

        // Returner resultatet
        return games;
    }

    public async Task<IEnumerable<Game>> GetByGameTimeAsync(DateTime gameTime)
    {
        _logger.LogDebug("Getting games by game time");

        // Bruk repository til å hente spill basert på spilltidspunktet
        var games = await _gameRepository.GetByGameTimeAsync(gameTime);

        // Returner resultatet
        return games;
    }

    public async Task<Game> GetByIdAsync(GameId id)
    {
        _logger.LogDebug("Getting game by ID");

        // Bruk repository til å hente et spill basert på ID
        var game = await _gameRepository.GetByIdAsync(id);

        // Returner resultatet
        return game;
    }


    public async Task<IEnumerable<Game>> GetByLocationAsync(string location)
    {
        _logger.LogDebug("Getting games by location");

        // Bruk repository til å hente spill basert på lokasjon
        var games = await _gameRepository.GetByLocationAsync(location);

        // Returner resultatet
        return games;
    }

    public async Task<IEnumerable<Game>> GetByOpponentNameAsync(string opponentName)
    {
        _logger.LogDebug("Getting games by opponent name");

        // Bruk repository til å hente spill basert på motstanderens navn
        var games = await _gameRepository.GetByOpponentNameAsync(opponentName);

        // Returner resultatet
        return games;
    }

    public async Task<Game> UpdateAsync(GameId id, Game game)
    {
        _logger.LogDebug("Updating game");

        // Sjekk om spillet med den gitte ID-en eksisterer
        var existingGame = await _gameRepository.GetByIdAsync(id);
        if (existingGame == null)
        {
            _logger.LogWarning("Game not found: {id}", id);
            return null;
        }

        // Oppdaterer egenskapene til det eksisterende spillet med egenskapene til det nye spillet
        existingGame.Location = game.Location;
        existingGame.OpponentName = game.OpponentName;
        existingGame.GameTime = game.GameTime;
        existingGame.Updated = DateTime.UtcNow;

        // Lagre endringene ved hjelp av repository
        var updatedGame = await _gameRepository.UpdateAsync(id, existingGame);

        // Returner det oppdaterte spillet
        return updatedGame;
    }
}
