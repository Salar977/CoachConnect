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
    public Task<TeamDTO?> CreateAsync(TeamDTO teamDTO)
    {
        throw new NotImplementedException();
    }

    public Task<TeamDTO?> DeleteAsync(TeamId id)
    {
        throw new NotImplementedException();
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

    public Task<TeamDTO?> GetByIdAsync(TeamId id)
    {
        _logger.LogDebug("Getting Team by id: {id}", id);

        var res = await _teamRepository.GetByIdAsync(id);
        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public Task<TeamDTO?> UpdateAsync(TeamId id, TeamDTO teamDto)
    {
        throw new NotImplementedException();
    }
}
