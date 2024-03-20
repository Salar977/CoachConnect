using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/teams")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly ILogger<TeamsController> _logger;

    [HttpGet(Name = "GetAllTeams")]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllGames([FromQuery] TeamQuery teamQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Teams");

        return Ok(await _teamService.GetAllAsync(teamQuery));
    }
    [HttpGet("{id}", Name = "GetTeamById")]

    public async Task<ActionResult<TeamDTO>> GetTeamById(Guid id)
    {
        _logger.LogDebug("Getting team by ID: {id}", id);

        var team = await _teamService.GetByIdAsync(new TeamId(id));
        return team != null ? Ok(team) : NotFound($"Team with ID '{id}' not found");
    }
    [HttpPost("register", Name = "CreateTeam")]
    public async Task<ActionResult<TeamDTO>> CreateTeam([FromBody] TeamDTO teamDTO)
    {
        _logger.LogDebug("Create new Team");

        var res = await _teamService.CreateAsync(teamDTO);
        return res != null ? Ok(res) : BadRequest("Could not Create new team");
    }

    [HttpDelete("{id}", Name = "DeleteTeam")]
    public async Task<ActionResult<TeamDTO>> DeleteTeam(Guid id)
    {
        _logger.LogDebug("Deleting team with ID: {id}", id);

        var res = await _teamService.DeleteAsync(new TeamId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete Team");
    }
    [HttpPut("{id}", Name = "UpdateTeam")]
    public async Task<ActionResult<TeamDTO>> UpdateTeam(Guid id, [FromBody] TeamDTO teamDTO)
    {
        _logger.LogDebug("Updating team with ID: {id}", id);

        var res = await _teamService.UpdateAsync(new TeamId(id), teamDTO);
        return res != null ? Ok(res) : BadRequest("Could not update Team");
    }
}
