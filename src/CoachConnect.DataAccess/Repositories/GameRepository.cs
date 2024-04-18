﻿using CoachConnect.DataAccess.Data;
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
    private readonly CoachConnectDbContext _dbContext; 
    private readonly ILogger<GameRepository> _logger; 
    public GameRepository(CoachConnectDbContext dbContext, ILogger<GameRepository> logger) 
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

        if (!string.IsNullOrWhiteSpace(gameQuery.HomeTeam))
        {
            games = games.Where(g => g.HomeTeam.StartsWith(gameQuery.HomeTeam));
        }

        if (!string.IsNullOrWhiteSpace(gameQuery.AwayTeam))
        {
            games = games.Where(g => g.AwayTeam.StartsWith(gameQuery.AwayTeam));
        }

        if (gameQuery.GameDate != null && gameQuery.GameDate != DateTime.MinValue)
        {
            games = games.Where(g => g.GameTime.Date == gameQuery.GameDate.Value.Date); // salar: gjort endring for å få dato get by date. Endret også navn til GameDate i GameQuery.cs
        }

        if (!string.IsNullOrWhiteSpace(gameQuery.SortBy))
        {
            if (gameQuery.SortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
            {
                games = gameQuery.IsDescending ? games.OrderByDescending(x => x.Location) : games.OrderBy(x => x.Location);
            }

            if (gameQuery.SortBy.Equals("Opponent name", StringComparison.OrdinalIgnoreCase))
            {
                games = gameQuery.IsDescending ? games.OrderByDescending(x => x.HomeTeam) : games.OrderBy(x => x.HomeTeam);
            }

            if (gameQuery.SortBy.Equals("Opponent name", StringComparison.OrdinalIgnoreCase))
            {
                games = gameQuery.IsDescending ? games.OrderByDescending(x => x.AwayTeam) : games.OrderBy(x => x.AwayTeam);
            }
        }

        var skipNumber = (gameQuery.PageNumber - 1) * gameQuery.PageSize;

        return await games
            .Skip(skipNumber)
            .Take(gameQuery.PageSize)
            .ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(GameId id)
    {
        _logger.LogDebug("Getting Game by id: {id} from db", id);

        return await _dbContext.Games.FindAsync(id);
    }

    public async Task<Game?> GetByGameTimeAsync(DateTime dateTime)
    {
        _logger.LogDebug("Getting Game by time: {dateTime} from db", dateTime);

        DateTime startDate = dateTime.Date;
        DateTime endDate = startDate.AddDays(1);

        return await _dbContext.Games
            .Where(d => d.GameTime >= startDate && d.GameTime < endDate)
            .FirstOrDefaultAsync();
    }
    public async Task<Game?> UpdateAsync(GameId id, Game game)
    {
        _logger.LogDebug("Updating Game: {id} in db", id);

        var gme = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id.Equals(id));
        if (gme == null) return null;

        gme.Location = string.IsNullOrEmpty(game.Location) ? gme.Location : game.Location;
        gme.HomeTeam = string.IsNullOrEmpty(game.HomeTeam) ? gme.HomeTeam : game.HomeTeam;
        gme.AwayTeam = string.IsNullOrEmpty(game.AwayTeam) ? gme.AwayTeam : game.AwayTeam;
        gme.GameTime = game.GameTime;
        gme.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return gme;
    }
}