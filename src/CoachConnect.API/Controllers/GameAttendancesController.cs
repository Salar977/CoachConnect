using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    // GET: api/<GameAttendanceController>      // husk å sette på endepunkter her og gamescontroller
    [HttpGet(Name = "GetAllGameAttendances")]

    public async Task<ActionResult<IEnumerable<GameAttendanceDTO>>> GetAllGameAttendances([FromQuery] GameAttendanceQuery gameAttendanceQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting GameAttendances");

        return Ok(await _gameAttendanceService.GetAllAsync(gameAttendanceQuery));
    }

    // POST api/<GameAttendanceController>
    [HttpPost("register", Name = "registerGameAttendance")]
    public async Task<ActionResult<GameAttendanceDTO>> RegisterGameAttendance([FromBody] GameAttendanceDTO gameAttendanceDTO)
    {
        _logger.LogDebug("Create new Gameattendance");

        var res = await _gameAttendanceService.RegisterGameAttendanceAsync(gameAttendanceDTO);
        return res != null ? Ok(res) : BadRequest("Could not register gameAttendance");
    }

    // PUT api/<GameAttendanceController>/5
    [HttpGet("{id}", Name = "GetGameAttendanceById")]
    public async Task<ActionResult<GameAttendanceDTO>> GetGameAttendanceById([FromRoute] Guid id) // bruk Guid her pga modelbinding kjenner ikke igjen vår custom UserId, så bruk Guid her og vi må konvertere under isteden
    {
        _logger.LogDebug("Getting gameattendance by id {id}", id);

        var res = await _gameAttendanceService.GetByIdAsync(new GameAttendanceId(id)); 
        return res != null ? Ok(res) : NotFound("Could not find any gameAttendance with this id");
    }

    // DELETE api/<GameAttendanceController>/5
    [HttpDelete("{id}", Name = "DeleteGameAttendance")]
    public async Task<ActionResult<GameAttendanceDTO>> DeleteGameAttendance([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting Gameattendance: {id}", id);

        var res = await _gameAttendanceService.DeleteAsync(new GameAttendanceId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete gameAttendance ID");
    }

    [HttpPut("{id}", Name = "UpdateGameAttendance")]
    public async Task<ActionResult<GameAttendanceDTO>> UpdateGameAttendance([FromRoute] Guid id, [FromBody] GameAttendanceDTO dto)
    {
        _logger.LogDebug("Updating Game Attendance: {id}", id);

        var res = await _gameAttendanceService.UpdateAsync(new GameAttendanceId(id), dto);
        return res != null ? Ok(res) : BadRequest("Could not update gameAttendance");
    }
}
