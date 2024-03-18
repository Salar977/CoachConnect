using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ILogger<TeamsController> _logger;
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllGames([FromQuery] TeamQuery teamQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Games");

        return Ok(await _teamService.GetAllAsync(teamQuery));
    }


}
