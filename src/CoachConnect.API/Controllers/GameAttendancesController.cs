using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
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

    // https://localhost:7036/api/v1/gameattendances
    [HttpGet(Name = "GetAllGameAttendances")]

    public async Task<ActionResult<IEnumerable<GameAttendanceDTO>>> GetAllGameAttendances([FromQuery] GameAttendanceQuery gameAttendanceQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting GameAttendances");

        return Ok(await _gameAttendanceService.GetAllAsync(gameAttendanceQuery));
    }

    // https://localhost:7036/api/v1/gameattendances/register
    [HttpPost("register", Name = "registerGameAttendance")]
    public async Task<ActionResult<GameAttendanceRegistrationDTO>> RegisterGameAttendance([FromBody] GameAttendanceRegistrationDTO gameAttendanceRegistrationDTO)
    {
        _logger.LogDebug("Create new Gameattendance");

        var res = await _gameAttendanceService.RegisterGameAttendanceAsync(gameAttendanceRegistrationDTO);
        return res != null ? Ok(res) : BadRequest("Could not register gameAttendance");
    }

    // https://localhost:7036/api/v1/gameattendances/8215514a-c2f8-46fd-a547-ab5c1fc76004
    [HttpGet("{id}", Name = "GetGameAttendanceById")]
    public async Task<ActionResult<GameAttendanceDTO>> GetGameAttendanceById([FromRoute] Guid id) 
    {
        _logger.LogDebug("Getting gameattendance by id {id}", id);

        var res = await _gameAttendanceService.GetByIdAsync(id); 
        return res != null ? Ok(res) : NotFound("Could not find any gameAttendance with this id");
    }

    // https://localhost:7036/api/v1/gameattendances/team/9f0a1b2c-3d4e-5f6a-7b8c-9d0e1f2a3019
    [HttpGet("team/{id}", Name = "GetGameAttendancesByTeamId")]
    public async Task<ActionResult<IEnumerable<GameAttendanceDTO>>> GetGameAttendancesByTeamId(Guid id)
    {
        _logger.LogDebug("Getting gameattendances by TeamID: {id}", id);

        var game = await _gameAttendanceService.GetGameAttendancesByTeamId(id);
        return game != null ? Ok(game) : NotFound($"Games with teamID '{id}' not found");
    }

    // https://localhost:7036/api/v1/gameattendances/aa15514a-c2f8-46fd-a547-ab5c1fc76e14
    [HttpDelete("{id}", Name = "DeleteGameAttendance")]
    public async Task<ActionResult<GameAttendanceDTO>> DeleteGameAttendance([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting Gameattendance: {id}", id);

        var res = await _gameAttendanceService.DeleteAsync(id);
        return res != null ? Ok(res) : BadRequest("Could not delete gameAttendance");
    }

    // https://localhost:7036/api/v1/gameattendances/3fa85f64-5717-4562-b3fc-2c963f66afa6
    //[HttpPut("{id}", Name = "UpdateGameAttendance")]
    //public async Task<ActionResult<GameAttendanceDTO>> UpdateGameAttendance([FromRoute] Guid id, [FromBody] GameAttendanceDTO dto)
    //{
    //    _logger.LogDebug("Updating Game Attendance: {id}", id);

    //    var res = await _gameAttendanceService.UpdateAsync(id, dto);
    //    return res != null ? Ok(res) : BadRequest("Could not update gameAttendance");
    //}
}
