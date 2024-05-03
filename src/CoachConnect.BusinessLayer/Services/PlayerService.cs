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
    private readonly IMapper<Player, PlayerDTO> _playerMapper;
    private readonly IMapper<Player, PlayerRequest> _playerReqMapper;
    private readonly IMapper<Player, PlayerUpdate> _playerUpdateMapper;
    private readonly ILogger<GameService> _logger;

    public PlayerService(IPlayerRepository playerRepository,
                       ITeamRepository teamRepository,
                       IMapper<Player, PlayerDTO> playerMapper,
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
    public async Task<PlayerDTO?> CreateAsync(PlayerRequest playerDTO)
    {
        _logger.LogDebug("Create new Player");


        var player = _playerReqMapper.MapToEntity(playerDTO);
        player.Id = PlayerId.NewId;

        var res = await _playerRepository.RegisterPlayerAsync(player);

        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<PlayerDTO?> DeleteAsync(PlayerId id)
    {
        _logger.LogDebug("Deleting Team: {id}", id);

        var res = await _playerRepository.DeleteAsync(id);
        return res != null ? _playerMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<PlayerDTO>> GetAllAsync(PlayerQuery playerQuery)
    {
        _logger.LogDebug("Getting all players");
        var res = await _playerRepository.GetAllAsync(playerQuery);
        return res.Select(team => _playerMapper.MapToDTO(team)).ToList();
    }

    public async Task<PlayerDTO?> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Get player by id: {id}", id);

        var res = await _playerRepository.GetByIdAsync(new PlayerId(id));

        return res != null ? _playerMapper.MapToDTO(res) : null;
    }




    public Task<TeamDTO?> GetByTeamIdAsync(TeamId teamid)
    {
        throw new NotImplementedException();
    }

    public async Task<PlayerDTO?> UpdateAsync(PlayerId id, PlayerUpdate playerupdate)
    {
        _logger.LogDebug("Updating Player: {id}", id);

        // husk at users (el admin) kun skal kunne eoppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
        // kanksje noe som : throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");

        var player = _playerUpdateMapper.MapToEntity(playerupdate);
        player.Id = id;

        var res = await _playerRepository.UpdateAsync(id, player);
        return res != null ? _playerMapper.MapToDTO(player) : null;
    }
}
