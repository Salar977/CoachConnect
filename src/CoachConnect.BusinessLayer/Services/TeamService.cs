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
public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper<Team, TeamDTO> _teamMapper;
    private readonly ILogger<TeamService> _logger;

    public TeamService(ITeamRepository teamRepository,
                       IMapper<Team, TeamDTO> teamMapper,
                       ILogger<TeamService> logger)
    {
        _teamRepository = teamRepository;
        _teamMapper = teamMapper;
        _logger = logger;
    }

    public async Task<TeamDTO?> CreateAsync(TeamDTO teamDTO)
    {
        _logger.LogDebug("Create new Team");
        

        var team = _teamMapper.MapToEntity(teamDTO);
        team.Id = TeamId.NewId;

        var res = await _teamRepository.RegisterTeamAsync(team);

        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public async Task<TeamDTO?> DeleteAsync(TeamId id)
    {
        _logger.LogDebug("Deleting Team: {id}", id);

        var res = await _teamRepository.DeleteAsync(id);
        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<TeamDTO>> GetAllAsync(TeamQuery teamQuery)
    {
        _logger.LogDebug("Getting all teams");
        var res = await _teamRepository.GetAllAsync(teamQuery);
        return res.Select(team => _teamMapper.MapToDTO(team)).ToList();
    }

    public Task<TeamDTO?> GetByCoachIdAsync(CoachId coachid)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamDTO?> GetByIdAsync(TeamId id)
    {
        _logger.LogDebug("Getting Team by id: {id}", id);

        var res = await _teamRepository.GetByIdAsync(id);
        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public Task<TeamDTO?> GetTeamsByCoachId(CoachId coachid)
    {
        throw new NotImplementedException();
    }

    public async Task<TeamDTO?> UpdateAsync(TeamId id, TeamDTO teamDto)
    {
        _logger.LogDebug("Updating Team: {id}", id);

        // husk at users (el admin) kun skal kunne eoppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
        // kanksje noe som : throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");

        var team = _teamMapper.MapToEntity(teamDto);
        team.Id = id;

        var res = await _teamRepository.UpdateAsync(id, team);
        return res != null ? _teamMapper.MapToDTO(team) : null;
    }
}
