using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/players")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(IPlayerService playerService, ILogger<PlayersController> logger)
    {
        _playerService = playerService;
        _logger = logger;
    }

    //[Authorize(Roles = "Admin, Coach, User")]
    // GET: https://localhost:7036/api/v1/players
    [HttpGet(Name = "GetAllPlayers")]
    public async Task<ActionResult<IEnumerable<PlayerResponse>>> GetAllPlayers([FromQuery] PlayerQuery playerQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Players");

        return Ok(await _playerService.GetAllAsync(playerQuery));
    }

    // GET:https://localhost:7036/api/v1/players/87654321-2345-2345-2345-123456789144
    [HttpGet("{id}", Name = "GetPlayerById")]
    public async Task<ActionResult<PlayerResponse>> GetPlayerById(Guid id)
    {
        _logger.LogDebug("Getting player by ID: {id}", id);

        var player = await _playerService.GetByIdAsync(id);
        return player != null ? Ok(player) : NotFound($"Player with ID '{id}' not found");
    }

    // GET https://localhost:7036/api/v1/players/player/UserId/12345678-90ab-cdef-1234-567890abcdef
    [HttpGet("player/UserId/{userId}", Name = "GetPlayersByUserId")]
    public async Task<ActionResult<IEnumerable<PlayerResponse>>> GetTeamsByUserId(Guid userId)
    {
        _logger.LogTrace("Getting arrangementRegisters by memberid");
        var res = await _playerService.GetPlayersByUserIdAsync(new UserId(userId));
        return res != null
            ? Ok(res)
            : NotFound("Could not any find any teams with this coachid");
    }
    // GET: https://localhost:7036/api/v1/players/player/TeamId/d3b5a3d1-e0f2-4bf6-a5c3-7e8d9f1a2013
    [HttpGet("player/TeamId/{teamId}", Name = "GetPlayersByCoachId")]
    public async Task<ActionResult<IEnumerable<PlayerResponse>>> GetPlayersByTeamId(Guid teamId)
    {
        _logger.LogTrace("Getting arrangementRegisters by memberid");
        var res = await _playerService.GetPlayersByTeamIdAsync(new TeamId(teamId));
        return res != null
            ? Ok(res)
            : NotFound("Could not any find any players with this teamid");
    }

    // POST: https://localhost:7036/api/v1/players/register
    [HttpPost("register", Name = "CreatePlayer")]
    public async Task<ActionResult<PlayerResponse>> CreatePlayer([FromBody] PlayerRequest playerReq)
    {
        _logger.LogDebug("Create new Player");

        var res = await _playerService.CreateAsync(playerReq);
        return res != null ? Ok(res) : BadRequest("Could not Create new player");
    }
    // PUT: https://localhost:7036/api/v1/players/
    [HttpPut("{id}", Name = "UpdatePlayer")]
    public async Task<ActionResult<PlayerUpdate>> UpdatePlayer(Guid id, [FromBody] PlayerUpdate playerUpdate)
    {
        _logger.LogDebug("Updating player with ID: {id}", id);

        var res = await _playerService.UpdateAsync(new PlayerId(id), playerUpdate);
        return res != null ? Ok(res) : BadRequest("Could not update Player");
    }

    // DEL: https://localhost:7036/api/v1/players/
    [HttpDelete("{id}", Name = "DeletePlayer")]
    public async Task<ActionResult<PlayerResponse>> DeletePlayer(Guid id)
    {
        _logger.LogDebug("Deleting player with ID: {id}", id);

        var res = await _playerService.DeleteAsync(new PlayerId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete Player");
    }
    

}
