using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/v1/coaches")]
[ApiController]
public class CoachesController : ControllerBase
{
    private readonly ICoachService _coachService;
    private readonly ILogger<CoachesController> _logger;    

    public CoachesController(ICoachService coachService ,ILogger<CoachesController> logger)
    {
        _coachService = coachService;
        _logger = logger;
    }

    // GET: https://localhost:7036/api/v1/coaches
    [HttpGet(Name = "GetCoaches")]
    public async Task<ActionResult<IEnumerable<CoachDTO>>> GetCoaches([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting coaches");

        return Ok(await _coachService.GetAllAsync(query));
    }

    // GET api/<CoachesController>/5 // fyll inn disse
    [HttpGet("{id}", Name = "GetCoachById")]
    public string Get(int id)
    {
        return "value";
    }

    // PUT api/<CoachesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CoachesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    // POST https://localhost:7036/api/v1/coaches/register
    [HttpPost("register", Name = "RegisterCoach")]
    public async Task<ActionResult<CoachDTO>> RegisterCoach([FromBody] CoachRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new coach: {dto}", dto);

        var res = await _coachService.RegisterCoachAsync(dto);
        return res != null ? Ok(res) : BadRequest("Could not register new coach");
    }
}
