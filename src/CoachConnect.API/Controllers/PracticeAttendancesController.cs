using CoachConnect.BusinessLayer.DTOs.PracticeAttendanceDtos;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin, Coach")]
    [HttpGet(Name = "GetAllAsync")]
    public async Task<ActionResult<IEnumerable<PracticeAttendanceResponse>>> GetAllAttendancesAsync([FromQuery] PracticeAttendanceQuery attendanceQuery)
    {
        _logger.LogInformation("Get all practices - Controller");
        return Ok(await _practiceAttendanceService.GetAllAsync(attendanceQuery));
    }
    [Authorize(Roles = "Admin, Coach")]
    [HttpGet("/api/v1/practice/{practiceId:guid}", Name = "GetAllByPracticeAsync")]
    public async Task<ActionResult<IEnumerable<PracticeAttendanceResponse>>> GetAllByPracticeAsync([FromRoute] Guid practiceId)
    {
        _logger.LogInformation("Getting all attendences for the practice - Controller");
        return Ok(await _practiceAttendanceService.GetByPracticeAsync(practiceId));
    }

    [Authorize(Roles = "Admin, Coach")]
    [HttpGet("{id:guid}", Name = "GetAttendanceByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> GetByIdAsync([FromRoute] Guid id)
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

    [Authorize(Roles = "Admin, Coach")]
    [HttpPost("register", Name = "RegisterAttendanceAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> RegisterAttendanceAsync([FromBody]
                                                                               PracticeAttendanceRequest
                                                                               practiceAttendanceRequest)
    {
        if(!ModelState.IsValid) { return BadRequest("Model is not valid"); }
        var attendance = await _practiceAttendanceService.RegisterPracticeAttendanceAsync(practiceAttendanceRequest);
        if (attendance is null) { return BadRequest("attendance is null"); }

        _logger.LogInformation("Attendance is added to practice - Controller");
        return Ok(attendance);
    }

    [Authorize(Roles = "Admin, Coach")]
    [HttpDelete("{id:guid}", Name = "DeleteAttendanceByIdAsync")]
    public async Task<ActionResult<PracticeAttendanceResponse>> DeleteByIdAsync([FromRoute] Guid id)
    {
        var deleteAttendance = await _practiceAttendanceService.DeleteByIdAsync(id);
        if(deleteAttendance is null) return BadRequest();

        _logger.LogInformation("Attendance is removed from practice - Controller");
        return Ok(deleteAttendance);
    }
}
