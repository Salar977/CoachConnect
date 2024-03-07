﻿using CoachConnect.DataAccess.Data;
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

        public async Task<ICollection<Game>> GetAllAsync(int page, int pageSize)
        {
            _logger.LogDebug("Getting Games from db");

            int itemsToSkip = (page - 1) * pageSize;

            var res = await _dbContext.Games
                .OrderBy(g => g.OpponentName)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .Distinct()
                .ToListAsync();

            return res;
        }

        public Task<ICollection<Game>> GetByGameTimeAsync(DateTime gameTime)
        {
            throw new NotImplementedException();
        }

        public async Task<Game?> GetByIdAsync(GameId id)
        {
            _logger.LogDebug("Getting Game by id: {id} from db", id);

            return await _dbContext.Games.FindAsync(id);
        }

        public Task<ICollection<Game>> GetByLocationAsync(string location)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Game>> GetByOpponentNameAsync(string opponentName)
        {
            _logger.LogDebug("Getting Game by opponent name: {opponentName} from db", opponentName);

            var res = await _dbContext.Games
                .Where(g => g.OpponentName
                .StartsWith(opponentName))
                .OrderBy(g => g.OpponentName) 
                .ToListAsync();

            return res;
        }

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
}