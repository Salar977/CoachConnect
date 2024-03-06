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

    // GET: api/<CoachesController> // fyll inn disse
    [HttpGet(Name = "GetCoaches")]
    public async Task<ActionResult<IEnumerable<CoachDTO>>> GetCoaches([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting coaches");

        return Ok(await _coachService.GetAllAsync(query));
    }

    // GET api/<CoachesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CoachesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
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
}
