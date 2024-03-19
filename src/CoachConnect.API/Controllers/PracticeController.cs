using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using CoachConnect.DataAccess.Entities;
using CoachConnect.BusinessLayer.DTOs.Practices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/practice")]
[ApiController]
public class PracticeController : ControllerBase
{
    private readonly IPracticeService _practiceService;
    private readonly ILogger<PracticeController> _logger;

    public PracticeController(IPracticeService practiceService,
                              ILogger<PracticeController> logger)
    {
        _practiceService = practiceService;
        _logger = logger;
    }
    // GET: api/<PracticeController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PracticeResponse>>> GetAll([FromQuery] PracticeQuery practiceQuery)
    {
        return Ok(await _practiceService.GetAllAsync(practiceQuery));
    }

    // GET api/<PracticeController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PracticeResponse>> GetById([FromRoute] Guid id)
    {
        var practice = await _practiceService.GetByIdAsync(new PracticeId(id));
        if(practice is null)
        {
            _logger.LogWarning("Practice not found");
            return NotFound("Practice not found");
        }
        return Ok(practice);
    }

    // POST api/<PracticeController>
    [HttpPost]
    public async Task<ActionResult<PracticeResponse>> CreatePractice([FromBody] PracticeRequest practice)
    {
        if (!ModelState.IsValid) return BadRequest();

        var createPractice = await _practiceService.RegisterPracticeAsync(practice);
        if (createPractice is null) { return BadRequest(); }

        return Ok(createPractice);
    }

    // PUT api/<PracticeController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PracticeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
