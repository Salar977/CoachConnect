﻿using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories;
public class GameAttendanceRepository : IGameAttendanceRepository
{
    private readonly ILogger<GameAttendanceRepository> _logger;
    private readonly CoachConnectDbContext _dbContext;
    public GameAttendanceRepository(ILogger<GameAttendanceRepository> logger, CoachConnectDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<GameAttendance?> DeleteAsync(GameAttendanceId id)
    {
        _logger.LogDebug("Deleting gameAttendance: {id} from db", id);

        var res = await _dbContext.Game_attendences.FindAsync(id);
        if (res == null) return null;

        _dbContext.Game_attendences.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<ICollection<GameAttendance>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting GameAttendances from db");

        var gameAttendances = _dbContext.Game_attendences.AsQueryable();

        if (!string.IsNullOrWhiteSpace(gameAttendanceQuery.PlayerLastName))
        {
            gameAttendances = gameAttendances.Where(g => g.Player.LastName.StartsWith(gameAttendanceQuery.PlayerLastName));
        }

        if (gameAttendanceQuery.GameId != null && gameAttendanceQuery.GameId != Guid.Empty)
        {
            var gameId = gameAttendanceQuery.GameId.Value; // Extract non-nullable Guid value
            gameAttendances = gameAttendances.Where(g => g.GameId == new GameId(gameId));
        }

        if (!string.IsNullOrWhiteSpace(gameAttendanceQuery.SortBy))
        {
            if (gameAttendanceQuery.SortBy.Equals("PlayerLastName", StringComparison.OrdinalIgnoreCase))
            {
                gameAttendances = gameAttendanceQuery.IsDescending ? gameAttendances.OrderByDescending(x => x.Player.LastName) : gameAttendances.OrderBy(x => x.Player.LastName);
            }         
        }

        var skipNumber = (gameAttendanceQuery.PageNumber - 1) * gameAttendanceQuery.PageSize;

        return await gameAttendances
            .Skip(skipNumber)
            .Take(gameAttendanceQuery.PageSize)
            .ToListAsync();
    }

    public async Task<GameAttendance?> GetByIdAsync(GameAttendanceId id)
    {
        _logger.LogDebug("Getting gameAttendance by id: {id} from db", id);

        return await _dbContext.Game_attendences.FindAsync(id);
    }

    public async Task<GameAttendance?> RegisterGameAttendanceAsync(GameAttendance gameAttendance)
    {
        _logger.LogDebug("Adding Gameattendance to DB");

        await _dbContext.Game_attendences.AddAsync(gameAttendance);
        await _dbContext.SaveChangesAsync();

        return gameAttendance;
    }

    public async Task<GameAttendance?> UpdateAsync(GameAttendanceId id, GameAttendance gameAttendance)
    {
        _logger.LogDebug("Updating gameAttendance: {id} in db", id);

        var gameAtt = await _dbContext.Game_attendences.FirstOrDefaultAsync(g => g.Id.Equals(id));
        if (gameAtt == null) return null;

        gameAtt.GameId = gameAttendance.GameId != GameId.Empty ? gameAttendance.GameId : gameAtt.GameId;
        gameAtt.PlayerId = gameAttendance.PlayerId != PlayerId.Empty ? gameAttendance.PlayerId : gameAtt.PlayerId;
        gameAtt.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return gameAtt;
    }

}
