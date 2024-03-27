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
public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper<Player, PlayerDTO> _playerMapper;
    private readonly ILogger<PlayerService> _logger;

    public PlayerService(IPlayerRepository playerRepository, IMapper<Player, PlayerDTO> playerMapper, ILogger<PlayerService> logger)
    {
        _playerRepository = playerRepository;
        _playerMapper = playerMapper;
        _logger = logger;
    }

    public async Task<PlayerDTO?> CreateAsync(PlayerDTO playerDTO)
    {
        _logger.LogDebug("Create new Player");


        var player = _playerMapper.MapToEntity(playerDTO);
        player.Id = PlayerId.NewId;

        var res = await _playerRepository.RegisterPlayerAsync(player);

        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<PlayerDTO?> DeleteAsync(PlayerId id)
    {
        _logger.LogDebug("Deleting Player: {id}", id);

        var res = await _playerRepository.DeleteAsync(id);
        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<PlayerDTO>> GetAllAsync(PlayerQuery playerQuery)
    {
        _logger.LogDebug("Getting all players");
        var res = await _playerRepository.GetAllAsync(playerQuery);
        return res.Select(player => _playerMapper.MapToDTO(player)).ToList();
    }

    public async Task<PlayerDTO?> GetByIdAsync(PlayerId id)
    {
        _logger.LogDebug("Getting Player by id: {id}", id);

        var res = await _playerRepository.GetByIdAsync(id);
        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public Task<ICollection<PlayerDTO?>> GetPlayersByTeamIdAsync(TeamId teamId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<PlayerDTO?>> GetPlayersByUserIdAsync(UserId userId)
    {
        _logger?.LogDebug("Get Player by User id");
        if (_playerRepository == null || _playerMapper == null)
        {
            throw new ApplicationException("Arrangement register repository or mapper is null.");
        }

        var players = await _playerRepository.GetPlayersByUserIdAsync(userId);

        if (players == null)
        {
            return new List<PlayerDTO>();
        }

        var dtos = players.Select(player => _playerMapper.MapToDTO(player)).ToList();
        return dtos;
    }



    public async Task<PlayerDTO?> UpdateAsync(PlayerId id, PlayerDTO playerDto)
    {
        _logger.LogDebug("Updating PLayer: {id}", id);

        var player = _playerMapper.MapToEntity(playerDto);
        player.Id = id;

        var res = await _playerRepository.UpdateAsync(id, player);
        return res != null ? _playerMapper.MapToDTO(player) : null;
    }
}
