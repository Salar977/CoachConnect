using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/v1/practice/attendances")]
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

    [HttpGet(Name = "GetAllAsync")]
    public async Task<ActionResult<IEnumerable<PracticeAttendanceResponse>>> GetAllAsync([FromQuery] PracticeAttendanceQuery attendanceQuery)
    {
        _logger.LogInformation("Get all practices - Controller");
        return Ok(await _practiceAttendanceService.GetAllAsync(attendanceQuery));
    }

    [HttpGet("/api/v1/practice/{practiceId:guid}", Name = "GetAllByPracticeAsync")]
    public async Task<ActionResult<IEnumerable<PracticeAttendanceResponse>>> GetAllByPractice([FromRoute] Guid practiceId)
    {
        _logger.LogInformation("Getting all attendences for the practice - Controller");
        return Ok(await _practiceAttendanceService.GetByPracticeAsync(practiceId));
    }


    [HttpGet("{id:guid}", Name = "GetAttendanceByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> GetById([FromRoute] Guid id)
    {
        var attendance = await _practiceAttendanceService.GetByIdAsync(id);
        if(attendance is null)
        {
            _logger.LogWarning("Attendance not found");
            return NotFound();
        }

        _logger.LogInformation("Returned attendance: {attendance}", attendance);
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

        _logger.LogInformation("Attendance is added to practice - Controller");
        return Ok(attendance);
    }

    [HttpDelete("{id:guid}", Name = "DeleteAttendanceByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> DeleteById([FromRoute] Guid id)
    {
        var deleteAttendance = await _practiceAttendanceService.DeleteByIdAsync(id);
        if(deleteAttendance is null) return BadRequest();

        _logger.LogInformation("Attendance is removed from practice - Controller");
        return Ok(deleteAttendance);
    }
}
