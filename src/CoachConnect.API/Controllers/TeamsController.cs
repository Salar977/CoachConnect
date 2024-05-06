using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/v1/teams")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly ILogger<TeamsController> _logger;

    public TeamsController(ITeamService teamService, ILogger<TeamsController> logger)
    {
        _teamService = teamService;
        _logger = logger;
    }

    [Authorize(Roles = "Admin, Coach, User")]
    // GET: https://localhost:7036/api/v1/teams
    [HttpGet(Name = "GetAllTeams")]
    public async Task<ActionResult<IEnumerable<TeamResponse>>> GetAllTeams([FromQuery] TeamQuery teamQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Teams");

        return Ok(await _teamService.GetAllAsync(teamQuery));
    }

    [Authorize(Roles = "Admin, Coach, User")]
    // GET: https://localhost:7036/api/v1/teams/9f0a1b2c-3d4e-5f6a-7b8c-9d0e1f2a3019
    [HttpGet("{id}", Name = "GetTeamById")]
    public async Task<ActionResult<TeamResponse>> GetTeamById(Guid id)
    {
        _logger.LogDebug("Getting team by ID: {id}", id);

        var team = await _teamService.GetByIdAsync(new TeamId(id));
        return team != null ? Ok(team) : NotFound($"Team with ID '{id}' not found");
    }

    [Authorize(Roles = "Admin, Coach, User")]
    // GET: https://localhost:7036/api/v1/teams/CoachId/
    [HttpGet("CoachId/{CoachId}", Name = "GetTeamsByCoachId")]
    public async Task<ActionResult<IEnumerable<TeamResponse>>> GetTeamsByCoachId(Guid CoachId)
    {
        _logger.LogTrace("Getting arrangementRegisters by memberid");
        var res = await _teamService.GetTeamsByCoachId(new CoachId(CoachId));
        return res != null
            ? Ok(res)
            : NotFound("Could not any find any teams with this coachid");
    }

    [Authorize(Roles = "Admin, Coach, User")]
    // POST: https://localhost:7036/api/v1/teams/register
    [HttpPost("register", Name = "CreateTeam")]
    public async Task<ActionResult<TeamResponse>> CreateTeam([FromBody] TeamRequest teamReq)
    {
        _logger.LogDebug("Create new Team");

        var res = await _teamService.CreateAsync(teamReq);
        return res != null ? Ok(res) : BadRequest("Could not Create new team");
    }

    [Authorize(Roles = "Admin, Coach")]
    // POST: https://localhost:7036/api/v1/teams/
    [HttpDelete("{id}", Name = "DeleteTeam")]
    public async Task<ActionResult<TeamResponse>> DeleteTeam(Guid id)
    {
        _logger.LogDebug("Deleting team with ID: {id}", id);

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        string idFromRoute = "TeamId { teamId = " + id.ToString() + " }";
        bool isAdmin = this.User.IsInRole("Admin");

        if (!isAdmin && !idFromToken.Equals(idFromRoute))
            return Unauthorized("No authorization to delete this team");

        var res = await _teamService.DeleteAsync(new TeamId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete Team");
    }

    [Authorize(Roles = "Admin, Coach")]
    // PUT: https://localhost:7036/api/v1/teams/028070a6-602c-4162-9a34-6a6fcd3ca4bd
    [HttpPut("{id}", Name = "UpdateTeam")]
    public async Task<ActionResult<TeamResponse>> UpdateTeam(Guid id, [FromBody] TeamUpdate teamupdate)
    {
        _logger.LogDebug("Updating team with ID: {id}", id);

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        string idFromRoute = "TeamId { teamId = " + id.ToString() + " }";
        bool isAdmin = this.User.IsInRole("Admin");

        if (!isAdmin && !idFromToken.Equals(idFromRoute))
            return Unauthorized("No authorization to update this team");
        var res = await _teamService.UpdateAsync(new TeamId(id), teamupdate);
        return res != null ? Ok(res) : BadRequest("Could not update Team");
    }
}
