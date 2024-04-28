using CoachConnect.BusinessLayer.DTOs.GameAttendances;
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
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper<GameAttendance, GameAttendanceDTO> _gameAttendanceMapper;
    private readonly IMapper<GameAttendance, GameAttendanceRegistrationDTO> _gameAttendanceRegistrationMapper;

    public GameAttendanceService(ILogger<GameAttendanceService> logger,
                                IGameAttendanceRepository gameAttendanceRepository,
                                IGameRepository gameRepository,
                                IPlayerRepository playerRepository,
                                ICoachRepository coachRepository,
                                ITeamRepository teamRepository,
                                IMapper<GameAttendance, GameAttendanceDTO> gameAttendanceMapper,
                                IMapper<GameAttendance, GameAttendanceRegistrationDTO> gameAttendanceRegistrationMapper)
    {
        _logger = logger;
        _gameAttendanceRepository = gameAttendanceRepository;
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _coachRepository = coachRepository;
        _teamRepository = teamRepository;
        _gameAttendanceMapper = gameAttendanceMapper;
        _gameAttendanceRegistrationMapper = gameAttendanceRegistrationMapper;
    }

    public async Task<ICollection<GameAttendanceDTO>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery)
    {
        _logger.LogDebug("Getting all gameattendances");
        var res = await _gameAttendanceRepository.GetAllAsync(gameAttendanceQuery);
        return res.Select(game => _gameAttendanceMapper.MapToDTO(game)).ToList();
    }

    public async Task<GameAttendanceDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting gameattendance by id: {id}", id);

        var gameAttendanceId = new GameAttendanceId(id);
        var res = await _gameAttendanceRepository.GetByIdAsync(gameAttendanceId);
        return res != null ? _gameAttendanceMapper.MapToDTO(res) : null;
    }

    public async Task<GameAttendanceRegistrationDTO?> RegisterGameAttendanceAsync(bool isAdmin, string idFromToken, GameAttendanceRegistrationDTO dto)
    {
        _logger.LogDebug("Create new Gameattendance");

        if (!isAdmin)
        {
            string idFromTokenBeforeExtraction = idFromToken; 

            int startIndex = idFromTokenBeforeExtraction.IndexOf('=') + 1;
            int length = idFromTokenBeforeExtraction.IndexOf('}') - startIndex;
            string idFromTokenExtracted = idFromToken.Substring(startIndex, length);

            if (Guid.TryParse(idFromTokenExtracted, out var coachGuid))
            {
                var coachId = new CoachId(coachGuid);

                var coach = await _coachRepository.GetByIdAsync(coachId);
                var returnedPlayer = await _playerRepository.GetByIdAsync(dto.PlayerId);
                var game = await _gameRepository.GetByIdAsync(dto.GameId);

                if (returnedPlayer == null || game == null || coach!.Id != returnedPlayer.Team?.CoachId || returnedPlayer.TeamId.teamId != game.HomeTeam.teamId
                    && returnedPlayer.TeamId.teamId != game.AwayTeam.teamId)
                {
                    return null; // custom ex
                }

                var attendanceExists = await _gameAttendanceRepository.CheckAttendanceExistsAsync(dto.PlayerId, dto.GameId);
                if (attendanceExists)
                {
                    return null; // custom ex
                }
            }
        }

        var gameAttendanceRegistration = _gameAttendanceRegistrationMapper.MapToEntity(dto);
        gameAttendanceRegistration.Id = GameAttendanceId.NewId;

        var player = await _playerRepository.GetByIdAsync(dto.PlayerId);
        if (player != null) { player.TotalGames++; }

        var res = await _gameAttendanceRepository.RegisterGameAttendanceAsync(gameAttendanceRegistration);

        return res != null ? _gameAttendanceRegistrationMapper.MapToDTO(res) : null;
    }

    public async Task<GameAttendanceDTO?> DeleteAsync(bool isAdmin, string idFromToken, Guid id)
    {
        _logger.LogDebug("Deleting Gameattendance: {id}", id);

        var gameAttendanceId = new GameAttendanceId(id);

        if (!isAdmin)
        {
            string idFromTokenBeforeExtraction = idFromToken;

            int startIndex = idFromTokenBeforeExtraction.IndexOf('=') + 1;
            int length = idFromTokenBeforeExtraction.IndexOf('}') - startIndex;
            string idFromTokenExtracted = idFromToken.Substring(startIndex, length);

            if (Guid.TryParse(idFromTokenExtracted, out var coachGuid))
            {
                var coachId = new CoachId(coachGuid);
                              
                var returnedGameAttendance = await _gameAttendanceRepository.GetByIdAsync(gameAttendanceId);
                if (returnedGameAttendance == null) return null;

                var game = await _gameRepository.GetByIdAsync(returnedGameAttendance.GameId); // denne må fikses!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                var homeTeamId = new TeamId(game!.HomeTeam.teamId);
                var awayTeamId = new TeamId(game!.AwayTeam.teamId);  

                var homeTeam = await _teamRepository.GetByIdAsync(homeTeamId);
                var awayTeam = await _teamRepository.GetByIdAsync(awayTeamId);

                if (homeTeam == null || awayTeam == null || (homeTeam.CoachId != coachId && awayTeam.CoachId != coachId))
                {
                    return null; // custom ex
                }

            }
        }

        var gameAttendance = await _gameAttendanceRepository.GetByIdAsync(gameAttendanceId);
        if (gameAttendance == null)
        {
            _logger.LogError("Could not delete Gameattendance");
            return null;
        }

        var player = await _playerRepository.GetByIdAsync(gameAttendance.PlayerId);
        if (player != null) { player.TotalGames--; }

        await _gameAttendanceRepository.DeleteAsync(gameAttendanceId);

        return gameAttendance != null ? _gameAttendanceMapper.MapToDTO(gameAttendance) : null;
    }
}
