using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using CoachConnect.BusinessLayer.DTOs.Practices;

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

    [HttpGet(Name = "GetAllPracticeAsync")]
    public async Task<ActionResult<IEnumerable<PracticeResponse>>> GetAllPractice([FromQuery] PracticeQuery practiceQuery)
    {
        _logger.LogInformation("Get all practices - Controller");
        return Ok(await _practiceService.GetAllAsync(practiceQuery));
    }

    [HttpGet("{id:guid}", Name = "GetPracticeByIdAsync")]
    public async Task<ActionResult<PracticeResponse>> GetById([FromRoute] Guid id)
    {
        var practice = await _practiceService.GetByIdAsync(id);


        if (practice is null)
        {
            _logger.LogWarning("Practice was not found with id: {id}", id);
            return NotFound("Practice was not found");
        }

        _logger.LogInformation("Retrieving practice.");
        return Ok(practice);
    }


    [HttpPost(Name = "CreatePracticeAsync")]
    public async Task<ActionResult<PracticeResponse>> CreatePractice([FromBody] PracticeRequest practice)
    {
        if (!ModelState.IsValid) return BadRequest();

        var createPractice = await _practiceService.RegisterPracticeAsync(practice);
        if (createPractice is null) { return BadRequest(); }

        _logger.LogInformation("Practice is created - Controller");
        return Ok(createPractice);
    }

    [HttpDelete("{id:guid}", Name = "DeletePracticeByIdAsync")]
    public async Task<ActionResult<PracticeResponse>> DeleteById([FromRoute] Guid id)
    {
        var practice = await _practiceService.GetByIdAsync(id);
        if (practice is null)
        {
            _logger.LogWarning("Practice was not found with id: {id}", id);
            return NotFound("Practice was not found");
        }

        await _practiceService.DeleteAsync(id);

        return Ok(practice);
    }

    [HttpPut("{id:guid}", Name = "UpdatePracticeByIdAsync")]
    public async Task<ActionResult<PracticeResponse>> UpdateById([FromRoute] Guid id,
                                                                 [FromBody] PracticeUpdate practiceUpdate)
    {
        var practice = await _practiceService.GetByIdAsync(id);
        if (practice is null)
        {
            _logger.LogWarning("Practice was not found with id: {id}", id);
            return NotFound("Practice was not found");
        }

        await _practiceService.UpdateAsync(id, practiceUpdate);

        return Ok(practice);
    }
}