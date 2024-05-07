using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;
public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper<Team, TeamResponse> _teamMapper;
    private readonly IMapper<Team, TeamRequest> _teamReqMapper;
    private readonly IMapper<Team, TeamUpdate> _teamUpdateMapper;
    private readonly ILogger<TeamService> _logger;

    public TeamService(ITeamRepository teamRepository,
                       IMapper<Team, TeamResponse> teamMapper,
                       IMapper<Team, TeamRequest> teamReqMapper,
                       IMapper<Team, TeamUpdate> teamUpdateMapper,
                       ILogger<TeamService> logger)
    {
        _teamRepository = teamRepository;
        _teamMapper = teamMapper;
        _teamReqMapper = teamReqMapper;
        _teamUpdateMapper = teamUpdateMapper;
        _logger = logger;
    }

    public async Task<TeamResponse?> CreateAsync(TeamRequest teamReq)
    {
        _logger.LogDebug("Create new Team");
        

        var team = _teamReqMapper.MapToEntity(teamReq);
        team.Id = TeamId.NewId;

        var res = await _teamRepository.RegisterTeamAsync(team);

        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public async Task<TeamResponse?> DeleteAsync(TeamId id)
    {
        _logger.LogDebug("Deleting Team: {id}", id);

        var res = await _teamRepository.DeleteAsync(id);
        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<TeamResponse>> GetAllAsync(TeamQuery teamQuery)
    {
        _logger.LogDebug("Getting all teams");
        var res = await _teamRepository.GetAllAsync(teamQuery);
        return res.Select(team => _teamMapper.MapToDTO(team)).ToList();
    }


    public async Task<TeamResponse?> GetByIdAsync(TeamId id)
    {
        _logger.LogDebug("Getting Team by id: {id}", id);

        var res = await _teamRepository.GetByIdAsync(id);
        return res != null ? _teamMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<TeamResponse?>> GetTeamsByCoachId(CoachId coachid)
    {
        _logger?.LogDebug("Get teams by coach id");
 
        if (_teamRepository == null || _teamMapper == null)
        {
            throw new ApplicationException("teams repository or mapper is null.");
        }

        var teams = await _teamRepository.GetByCoachIdAsync(coachid);

        // Check if the member ID exists
        if (teams == null)
        {
            return new List<TeamResponse?>();
        }

        // Map the result to DTOs
        var dtos = teams.Select(register => _teamMapper.MapToDTO(register)).ToList();
        return dtos;

    }

    public async Task<TeamResponse?> UpdateAsync(TeamId id, TeamUpdate teamupdate)
    {
        _logger.LogDebug("Updating Team: {id}", id);

        var team = _teamUpdateMapper.MapToEntity(teamupdate);
        team.Id = id;

        var res = await _teamRepository.UpdateAsync(id, team);
        return res != null ? _teamMapper.MapToDTO(team) : null;
    }
}
