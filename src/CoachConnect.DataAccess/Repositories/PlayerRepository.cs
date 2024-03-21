﻿using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;
public class PlayerRepository : IPlayerRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger <PlayerRepository> _logger;

    public PlayerRepository(ILogger<PlayerRepository> logger)
    {
        _logger = logger;
    }

    public async Task<Player?> DeleteAsync(PlayerId id)
    {
        _logger.LogDebug("Deleting Player: {id} from db", id);

        var res = await _dbContext.Players.FindAsync(id);
        if (res == null) return null;

        _dbContext.Players.Remove(res);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<ICollection<Player>> GetAllAsync(PlayerQuery playerQuery)
    {
        _logger.LogDebug("Getting Players from db");

        var players = _dbContext.Players.AsQueryable();

        if (!string.IsNullOrWhiteSpace(playerQuery.FirstName))
        {
            players = players.Where(g => g.FirstName.Contains(playerQuery.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(playerQuery.LastName))
        {
            players = players.Where(g => g.LastName.Contains(playerQuery.LastName));
        }

        if (playerQuery.Created != null && playerQuery.Created != DateTime.MinValue)
        {
            players = players.Where(g => g.Created == playerQuery.Created);
        }
        if (!string.IsNullOrWhiteSpace(playerQuery.SortBy))
        {
            if (playerQuery.SortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
            {
                players = playerQuery.IsDescending ? players.OrderByDescending(x => x.FirstName) : players.OrderBy(x => x.FirstName);
            }

            if (playerQuery.SortBy.Equals("Opponent name", StringComparison.OrdinalIgnoreCase))
            {
                players = playerQuery.IsDescending ? players.OrderByDescending(x => x.FirstName) : players.OrderBy(x => x.LastName);
            }
        }

        var skipNumber = (playerQuery.PageNumber - 1) * playerQuery.PageSize;

        return await players
            .Skip(skipNumber)
            .Take(playerQuery.PageSize)
            .ToListAsync();
    }

    public async Task<Player?> GetByIdAsync(PlayerId id)
    {
        _logger.LogDebug("Getting team by id: {id} from db", id);

        return await _dbContext.Players.FindAsync(id);
    }

    public async Task<Player?> GetByPlayerFirstNameAsync(string player)
    {
        _logger.LogDebug("Getting user by email: {email} from db", player);

        var res = await _dbContext.Players.FirstOrDefaultAsync(u => u.FirstName.Equals(player));
        return res;
    }

    public async Task<ICollection<Player?>> GetByTeamsIdAsync(TeamId teamId)
    {
        return await _dbContext.Players
            .Where(x => x.TeamId == teamId)
            .ToListAsync();
    }


    public async Task<ICollection<Player?>> GetByUsersByIdAsync(UserId userId)
    {
        return await _dbContext.Players
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<Player?> RegisterPlayerAsync(Player player)
    {
        _logger.LogDebug("Adding Game to DB");

        await _dbContext.Players.AddAsync(player);
        await _dbContext.SaveChangesAsync();

        return player;
    }

    public async Task<Player?> UpdateAsync(PlayerId id, Player player)
    {
        _logger.LogDebug("Updating Player: {id} in db", id);

        var playr = await _dbContext.Players.FirstOrDefaultAsync(g => g.Id.Equals(id));
        if (playr == null) return null;

        playr.FirstName = string.IsNullOrEmpty(player.FirstName) ? playr.FirstName : player.FirstName;
        playr.LastName = string.IsNullOrEmpty(player.LastName) ? playr.LastName : player.LastName;
        playr.Created = player.Created;
        playr.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return playr;
    }
}
