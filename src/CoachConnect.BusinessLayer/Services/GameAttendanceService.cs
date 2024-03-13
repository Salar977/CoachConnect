using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories;
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

    public async Task<GameAttendanceDTO?> DeleteAsync(GameAttendanceId id)
    {
        _logger.LogDebug("Deleting GameAttendance: {id}", id);

        var res = await _gameAttendanceRepository.DeleteAsync(id);
        return res != null ? _gameAttendanceMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<GameAttendanceDTO>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting all games");
        var res = await _gameAttendanceRepository.GetAllAsync(gameAttendanceQuery);
        return res.Select(game => _gameAttendanceMapper.MapToDTO(game)).ToList();
    }

    public async Task<GameAttendanceDTO?> GetByIdAsync(GameAttendanceId id)
    {
        _logger.LogDebug("Getting gameAttendance by id: {id}", id);

        var res = await _gameAttendanceRepository.GetByIdAsync(id);
        return res != null ? _gameAttendanceMapper.MapToDTO(res) : null;
    }

    public async Task<GameAttendanceDTO?> RegisterGameAttendanceAsync(GameAttendanceDTO dto)
    {
        _logger.LogDebug("Create new Gameattendance");
        //Husk legge til sjekke om kampen finnes fra før dersom ikke så legge til ny kamp

        var gameAttendance = _gameAttendanceMapper.MapToEntity(dto);
        gameAttendance.Id = GameAttendanceId.NewId;

        var res = await _gameAttendanceRepository.RegisterGameAttendanceAsync(gameAttendance);

        return res != null ? _gameAttendanceMapper.MapToDTO(res) : null;
    }

    public async Task<GameAttendanceDTO?> UpdateAsync(GameAttendanceId id, GameAttendanceDTO dto)
    {
        _logger.LogDebug("Updating gameAttendance: {id}", id);

        var gameAttendance = _gameAttendanceMapper.MapToEntity(dto);
        gameAttendance.Id = id;

        var res = await _gameAttendanceRepository.UpdateAsync(id, gameAttendance);
        return res != null ? _gameAttendanceMapper.MapToDTO(gameAttendance) : null;
    }
}
