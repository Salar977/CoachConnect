using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using CoachConnect.DataAccess.Entities;
using CoachConnect.BusinessLayer.DTOs.Practices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/practices")]
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PracticeResponse>>> GetAllPractice([FromQuery] PracticeQuery practiceQuery)
    {
        _logger.LogInformation("Retrieving all practices.");
        return Ok(await _practiceService.GetAllAsync(practiceQuery));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PracticeResponse>> GetById([FromRoute] Guid id)
    {
        var practice = await _practiceService.GetByIdAsync(id);

        if(practice is null)
        {
            _logger.LogWarning("Practice not found");
            return NotFound("Practice not found");
        }

        _logger.LogInformation("Retrieving practice.");
        return Ok(practice);
    }

    [HttpPost]
    public async Task<ActionResult<PracticeResponse>> CreatePractice([FromBody] PracticeRequest practice)
    {
        if (!ModelState.IsValid) return BadRequest();

        var createPractice = await _practiceService.RegisterPracticeAsync(practice);
        if (createPractice is null) { return BadRequest(); }

        _logger.LogInformation("Practice is created - Controller");
        return Ok(createPractice);
    }

    [HttpDelete]
    public async Task<ActionResult<PracticeResponse>> DeleteById([FromQuery] Guid id)
    {
        var practice = await _practiceService.GetByIdAsync(new PracticeId(id));

        if(practice is null) return NotFound();

        await _practiceService.DeleteAsync(new PracticeId(id));

        return Ok(practice);
    }
}
