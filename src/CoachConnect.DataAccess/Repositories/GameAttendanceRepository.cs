using CoachConnect.DataAccess.Data;
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

    public async Task<ICollection<GameAttendance>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting GameAttendances from db");

        var gameAttendances = _dbContext.Game_attendences.AsQueryable();

        if (!string.IsNullOrWhiteSpace(gameAttendanceQuery.PlayerLastName))
        {
            gameAttendances = gameAttendances.Where(g => g.Player.LastName.Contains(gameAttendanceQuery.PlayerLastName));
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
}
