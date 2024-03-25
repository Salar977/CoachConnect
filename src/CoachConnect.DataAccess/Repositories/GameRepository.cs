using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly CoachConnectDbContext _dbContext; // Deklarerer et privat medlemsfelt som holder referansen til databasetilgangskonteksten.
    private readonly ILogger<GameRepository> _logger; // Deklarerer et privat medlemsfelt som holder referansen til en logger for denne klassen.

    public GameRepository(CoachConnectDbContext dbContext, ILogger<GameRepository> logger) // Konstruktør som tar en databasekontekst og en logger som argumenter og tilordner dem til de tilsvarende medlemsfeltene.
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Game?> CreateAsync(Game game)
    {
        _logger.LogDebug("Adding Game to DB");

        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();

        return game;
    }

    public async Task<Game?> DeleteAsync(GameId id)
    {
        _logger.LogDebug("Deleting Game: {id} from db", id);

        var res = await _dbContext.Games.FindAsync(id);
        if (res == null) return null;

        _dbContext.Games.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<ICollection<Game>> GetAllAsync(GameQuery gameQuery)
    {
        _logger.LogDebug("Getting Games from db");

        var games = _dbContext.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(gameQuery.Location))
            {
                games = games.Where(g => g.Location.StartsWith(gameQuery.Location));
            }

            if (!string.IsNullOrWhiteSpace(gameQuery.OpponentName))
            {
                games = games.Where(g => g.OpponentName.StartsWith(gameQuery.OpponentName));
            }

        if (gameQuery.GameTime != null && gameQuery.GameTime != DateTime.MinValue)
        {
            games = games.Where(g => g.GameTime == gameQuery.GameTime);
        }

        if (!string.IsNullOrWhiteSpace(gameQuery.SortBy))
        {
            if (gameQuery.SortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
            {
                games = gameQuery.IsDescending ? games.OrderByDescending(x => x.Location) : games.OrderBy(x => x.Location);
            }

            if (gameQuery.SortBy.Equals("Opponent name", StringComparison.OrdinalIgnoreCase))
            {
                games = gameQuery.IsDescending ? games.OrderByDescending(x => x.OpponentName) : games.OrderBy(x => x.OpponentName);
            }
        }

        var skipNumber = (gameQuery.PageNumber - 1) * gameQuery.PageSize;

        return await games
            .Skip(skipNumber)
            .Take(gameQuery.PageSize)
            .ToListAsync();
    }
    //public async Task<ICollection<Game>> GetAllAsync(int page, int pageSize)
    //{
    //    _logger.LogDebug("Getting Games from db");

    //    int itemsToSkip = (page - 1) * pageSize;

    //    var res = await _dbContext.Games
    //        .OrderBy(g => g.OpponentName)
    //        .Skip(itemsToSkip)
    //        .Take(pageSize)
    //        .Distinct()
    //        .ToListAsync();

    //    return res;
    //}

    //public Task<ICollection<Game>> GetByGameTimeAsync(DateTime gameTime)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<Game?> GetByIdAsync(GameId id)
    {
        _logger.LogDebug("Getting Game by id: {id} from db", id);

        return await _dbContext.Games.FindAsync(id);
    }

    public async Task<Game?> GetByGameTimeAsync(DateTime dateTime)
    {
        _logger.LogDebug("Getting Game by time: {dateTime} from db", dateTime);

        // Get the start and end of the specified date
        DateTime startDate = dateTime.Date;
        DateTime endDate = startDate.AddDays(1);

        return await _dbContext.Games
            .Where(d => d.GameTime >= startDate && d.GameTime < endDate)
            .FirstOrDefaultAsync();
    }

    //public Task<ICollection<Game>> GetByLocationAsync(string location)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<ICollection<Game>> GetByOpponentNameAsync(string opponentName)
    //{
    //    _logger.LogDebug("Getting Game by opponent name: {opponentName} from db", opponentName);

    //    var res = await _dbContext.Games
    //        .Where(g => g.OpponentName
    //        .StartsWith(opponentName))
    //        .OrderBy(g => g.OpponentName) 
    //        .ToListAsync();

    //    return res;
    //}

    public async Task<Game?> UpdateAsync(GameId id, Game game)
    {
        _logger.LogDebug("Updating Game: {id} in db", id);

        var gme = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id.Equals(id));
        if (gme == null) return null;

        gme.Location = string.IsNullOrEmpty(game.Location) ? gme.Location : game.Location;
        gme.OpponentName = string.IsNullOrEmpty(game.OpponentName) ? gme.OpponentName : game.OpponentName;
        gme.GameTime = game.GameTime;
        gme.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return gme;
    }
}