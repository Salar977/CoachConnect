using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
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
    [HttpGet]

    public async Task<ActionResult<IEnumerable<GameAttendanceDTO>>> GetAllGameAttendances([FromQuery] GameAttendanceQuery gameAttendanceQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting GameAttendances");

        return Ok(await _gameAttendanceService.GetAllAsync(gameAttendanceQuery));
    }

    // GET api/<GameAttendanceController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<GameAttendanceController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<GameAttendanceController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<GameAttendanceController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
