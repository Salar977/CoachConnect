using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services;
public class GameAttendanceService : IGameAttendanceService
{
    private readonly ILogger<GameAttendanceService> _logger;
    private readonly IGameAttendanceRepository _gameAttendanceRepository;
    private readonly IMapper<GameAttendance, GameAttendanceDTO> _gameAttendanceMapper;

    public GameAttendanceService(ILogger<GameAttendanceService> logger, IGameAttendanceRepository gameAttendanceRepository, IMapper<GameAttendance, GameAttendanceDTO> gameAttendanceMapper)
    {
        _logger = logger;
        _gameAttendanceRepository = gameAttendanceRepository;
        _gameAttendanceMapper = gameAttendanceMapper;
    }

    public async Task<ICollection<GameAttendanceDTO>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting all games");
        var res = await _gameAttendanceRepository.GetAllAsync(gameAttendanceQuery);
        return res.Select(game => _gameAttendanceMapper.MapToDTO(game)).ToList();
    }
}
