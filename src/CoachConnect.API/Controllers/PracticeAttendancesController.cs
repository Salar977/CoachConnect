using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/practice/attendances")]
[ApiController]

public class PracticeAttendancesController : ControllerBase
{
    private readonly IPracticeAttendanceService _practiceAttendanceService;
    private readonly ILogger<PracticeAttendancesController> _logger;

    public PracticeAttendancesController(IPracticeAttendanceService practiceAttendanceService,
                                         ILogger<PracticeAttendancesController> logger)
    {
        _practiceAttendanceService = practiceAttendanceService;
        _logger = logger;
    }

    [HttpGet("/api/{practiceId:guid}/attendances", Name = "GetAllByPracticeAsync")]
    public async Task<ActionResult<IEnumerable<PracticeAttendanceResponse>>> GetAllByPractice([FromRoute] Guid practiceId)
    {
        _logger.LogInformation("Getting all attendences for the practice - Controller");
        return Ok(await _practiceAttendanceService.GetByPracticeAsync(practiceId));
    }


    [HttpGet("{id:guid}", Name = "GetByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> GetById([FromRoute] Guid id)
    {
        var attendance = await _practiceAttendanceService.GetByIdAsync(id);
        if(attendance is null)
        {
            return NotFound();
        }
        return Ok(attendance);
    }


    [HttpPost("register", Name = "AddAttendanceAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> AddAttendance([FromBody]
                                                                               PracticeAttendanceRequest
                                                                               practiceAttendanceRequest)
    {
        if(!ModelState.IsValid) { return BadRequest("Model is not valid"); }
        var attendance = await _practiceAttendanceService.RegisterPracticeAttendanceAsync(practiceAttendanceRequest);
        if (attendance is null) { return BadRequest("attendance is null"); }

        return Ok(attendance);
    }

    [HttpDelete("{id:guid}", Name = "DeleteByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> DeleteById([FromQuery] Guid id)
    {
        var deleteAttendance = await _practiceAttendanceService.DeleteByIdAsync(id);
        if(deleteAttendance is null) return BadRequest();

        return Ok(deleteAttendance);
    }
}
