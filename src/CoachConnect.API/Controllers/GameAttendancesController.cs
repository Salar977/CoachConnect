using CoachConnect.BusinessLayer.DTOs.GameAttendances;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/v1/gameattendances")]
[ApiController]
public class GameAttendancesController : ControllerBase
{
    private readonly IGameAttendanceService _gameAttendanceService;
    private readonly ILogger<GameAttendancesController> _logger;

    public GameAttendancesController(IGameAttendanceService gameAttendanceService, ILogger<GameAttendancesController> logger)
    {
        _gameAttendanceService = gameAttendanceService;
        _logger = logger;
    }

    // [Authorize(Roles = "Admin, Coach")]
    // https://localhost:7036/api/v1/gameattendances
    [HttpGet(Name = "GetAllGameAttendances")]
    public async Task<ActionResult<IEnumerable<GameAttendanceDTO>>> GetAllGameAttendances([FromQuery] GameAttendanceQuery gameAttendanceQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting GameAttendances");

        return Ok(await _gameAttendanceService.GetAllAsync(gameAttendanceQuery));
    }

    // [Authorize(Roles = "Admin")]
    // https://localhost:7036/api/v1/gameattendances/8215514a-c2f8-46fd-a547-ab5c1fc76004
    [HttpGet("{id}", Name = "GetGameAttendanceById")]
    public async Task<ActionResult<GameAttendanceDTO>> GetGameAttendanceById([FromRoute] Guid id)
    {
        _logger.LogDebug("Getting gameattendance by id {id}", id);

        var res = await _gameAttendanceService.GetByIdAsync(id);
        return res != null ? Ok(res) : NotFound("Could not find any gameAttendance with this id");
    }

    // [Authorize(Roles = "Admin, Coach")]
    // https://localhost:7036/api/v1/gameattendances/register
    [HttpPost("register", Name = "registerGameAttendance")]
    public async Task<ActionResult<GameAttendanceRegistrationDTO>> RegisterGameAttendance([FromBody] GameAttendanceRegistrationDTO gameAttendanceRegistrationDTO)
    {
        _logger.LogDebug("Create new Gameattendance");

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        bool isAdmin = this.HttpContext.User.IsInRole("Admin");

        var res = await _gameAttendanceService.RegisterGameAttendanceAsync(isAdmin, idFromToken, gameAttendanceRegistrationDTO);
        return res != null ? Ok(res) : BadRequest("Could not register gameAttendance");
    }

     [Authorize(Roles = "Admin, Coach")]
    // https://localhost:7036/api/v1/gameattendances/aa15514a-c2f8-46fd-a547-ab5c1fc76e14
    [HttpDelete("{id}", Name = "DeleteGameAttendance")]
    public async Task<ActionResult<GameAttendanceDTO>> DeleteGameAttendance([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting Gameattendance: {id}", id);

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        bool isAdmin = this.HttpContext.User.IsInRole("Admin");

        var res = await _gameAttendanceService.DeleteAsync(isAdmin, idFromToken, id);
        return res != null ? Ok(res) : BadRequest("Could not delete gameAttendance");
    }
}
