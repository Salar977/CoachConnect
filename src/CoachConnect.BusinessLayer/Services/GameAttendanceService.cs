using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.GameMappers;
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
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper<GameAttendance, GameAttendanceDTO> _gameAttendanceMapper;
    private readonly IMapper<GameAttendance, GameAttendanceRegistrationDTO> _gameAttendanceRegistrationMapper;

    public GameAttendanceService(ILogger<GameAttendanceService> logger,
                                IGameAttendanceRepository gameAttendanceRepository,
                                IPlayerRepository playerRepository,
                                IMapper<GameAttendance, GameAttendanceDTO> gameAttendanceMapper,
                                IMapper<GameAttendance, GameAttendanceRegistrationDTO> gameAttendanceRegistrationMapper)
    {
        _logger = logger;
        _gameAttendanceRepository = gameAttendanceRepository;
        _playerRepository = playerRepository;
        _gameAttendanceMapper = gameAttendanceMapper;
        _gameAttendanceRegistrationMapper = gameAttendanceRegistrationMapper;
    }

    public async Task<GameAttendanceDTO?> DeleteAsync(Guid id)
    {
        _logger.LogDebug("Deleting GameAttendance: {id}", id);

        var gameAttendanceId = new GameAttendanceId(id);
        
        var gameAttendance = await _gameAttendanceRepository.GetByIdAsync(gameAttendanceId);
        if (gameAttendance == null)
        {
            _logger.LogError("Could not delete GameAttendance");
            return null;
        }

        var player = await _playerRepository.GetByIdAsync(gameAttendance.PlayerId);
        if (player != null) { player.TotalGames--; }

        await _gameAttendanceRepository.DeleteAsync(gameAttendanceId);

        return gameAttendance!= null ? _gameAttendanceMapper.MapToDTO(gameAttendance) : null;
    }

    public async Task<ICollection<GameAttendanceDTO>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting all games");
        var res = await _gameAttendanceRepository.GetAllAsync(gameAttendanceQuery);
        return res.Select(game => _gameAttendanceMapper.MapToDTO(game)).ToList();
    }

    public async Task<GameAttendanceDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting gameAttendance by id: {id}", id);

        var gameAttendanceId = new GameAttendanceId(id);
        var res = await _gameAttendanceRepository.GetByIdAsync(gameAttendanceId);
        return res != null ? _gameAttendanceMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<GameAttendanceDTO>> GetGameAttendancesByTeamId(Guid id)
    {
        _logger.LogDebug("Getting Gameattendances by teamid: {id}", id);

        var teamId = new TeamId(id);
        var gameAttendances = await _gameAttendanceRepository.GetGameAttendancesByTeamId(teamId);

        if (gameAttendances != null)
        {
            var filteredGameAttendances = gameAttendances
                .Where(gameAttendance => gameAttendance.Game.HomeTeam == teamId || gameAttendance.Game.AwayTeam == teamId)
                .Select(game => _gameAttendanceMapper.MapToDTO(game))
                .ToList();

            return filteredGameAttendances;
        }
        else
        {
            _logger.LogInformation("No games found for team ID: {id}", id);
            return new List<GameAttendanceDTO>();
        }
    }

    public async Task<GameAttendanceRegistrationDTO?> RegisterGameAttendanceAsync(GameAttendanceRegistrationDTO dto)
    {
        _logger.LogDebug("Create new Gameattendance");

        var gameAttendanceRegistration = _gameAttendanceRegistrationMapper.MapToEntity(dto);
        gameAttendanceRegistration.Id = GameAttendanceId.NewId;

        var player = await _playerRepository.GetByIdAsync(dto.PlayerId);
        if (player != null) { player.TotalGames++; }

        var res = await _gameAttendanceRepository.RegisterGameAttendanceAsync(gameAttendanceRegistration);

        return res != null ? _gameAttendanceRegistrationMapper.MapToDTO(res) : null;
    }

    //public async Task<GameAttendanceDTO?> UpdateAsync(Guid id, GameAttendanceDTO dto)
    //{
    //    _logger.LogDebug("Updating gameAttendance: {id}", id);

    //    var gameAttendanceId = new GameAttendanceId(id);
    //    var gameAttendance = _gameAttendanceMapper.MapToEntity(dto);
    //    gameAttendance.Id = gameAttendanceId;

    //    var res = await _gameAttendanceRepository.UpdateAsync(gameAttendanceId, gameAttendance);
    //    return res != null ? _gameAttendanceMapper.MapToDTO(gameAttendance) : null;
    //}
}
