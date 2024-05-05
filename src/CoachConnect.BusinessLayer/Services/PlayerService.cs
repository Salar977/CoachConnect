using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Mappers.Practices;
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
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper<Player, PlayerResponse> _playerMapper;
    private readonly IMapper<Player, PlayerRequest> _playerReqMapper;
    private readonly IMapper<Player, PlayerUpdate> _playerUpdateMapper;
    private readonly ILogger<GameService> _logger;

    public PlayerService(IPlayerRepository playerRepository,
                       ITeamRepository teamRepository,
                       IMapper<Player, PlayerResponse> playerMapper,
                       IMapper<Player, PlayerRequest> playerReqMapper,
                       IMapper<Player, PlayerUpdate> playerUpdateMapper,
                       ILogger<GameService> logger)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
        _playerMapper = playerMapper;
        _playerReqMapper = playerReqMapper;
        _playerUpdateMapper = playerUpdateMapper;
        _logger = logger;
    }
    public async Task<PlayerResponse?> CreateAsync(PlayerRequest playerDTO)
    {
        _logger.LogDebug("Create new Player");


        var player = _playerReqMapper.MapToEntity(playerDTO);
        player.Id = PlayerId.NewId;

        var res = await _playerRepository.RegisterPlayerAsync(player);

        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<PlayerResponse?> DeleteAsync(PlayerId id)
    {
        _logger.LogDebug("Deleting Team: {id}", id);

        var res = await _playerRepository.DeleteAsync(id);
        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<PlayerResponse>> GetAllAsync(PlayerQuery playerQuery)
    {
        _logger.LogDebug("Getting all players");
        var res = await _playerRepository.GetAllAsync(playerQuery);
        return res.Select(team => _playerMapper.MapToDTO(team)).ToList();
    }

    public async Task<PlayerResponse?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Get player by id: {id}", id);

        var res = await _playerRepository.GetByIdAsync(new PlayerId(id));

        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<PlayerResponse?>> GetPlayersByTeamIdAsync(TeamId teamId)
    {
        _logger?.LogDebug("Get Players by team id");
        // Check for null before using the repository and mapper
        if (_playerRepository == null || _playerMapper == null)
        {
            throw new ApplicationException("Player repository or mapper is null.");
        }

        // Retrieve players by team ID
        var players = await _playerRepository.GetPlayersByTeamIdAsync(teamId);

        // Check if the team ID exists
        if (players == null)
        {
            return new List<PlayerResponse?>();
        }

        // Map the result to DTOs
        var dtos = players.Select(register => _playerMapper.MapToDTO(register)).ToList();
        return dtos;

    }

    public async Task<ICollection<PlayerResponse?>> GetPlayersByUserIdAsync(UserId userId)
    {
        _logger?.LogDebug("Get players by user id");
        // Check for null before using the repository and mapper
        if (_playerRepository == null || _playerMapper == null)
        {
            throw new ApplicationException("Arrangement register repository or mapper is null.");
        }

        // Retrieve arrangement registers by user ID
        var players = await _playerRepository.GetPlayersByUserIdAsync(userId);

        // Check if the member ID exists
        if (players == null)
        {
            return new List<PlayerResponse?>();
        }

        // Map the result to DTOs
        var dtos = players.Select(register => _playerMapper.MapToDTO(register)).ToList();
        return dtos;

    }

    public async Task<PlayerResponse?> UpdateAsync(PlayerId id, PlayerUpdate playerupdate)
    {
        _logger.LogDebug("Updating Player: {id}", id);

        var player = _playerUpdateMapper.MapToEntity(playerupdate);
        player.Id = id;

        var res = await _playerRepository.UpdateAsync(id, player);
        return res != null ? _playerMapper.MapToDTO(player) : null;
    }
}
