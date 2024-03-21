using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

    // GET api/<PracticeAttendancesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PracticeAttendanceResponse>> GetById([FromQuery] Guid id)
    {
        var attendance = await _practiceAttendanceService.GetByIdAsync(id);
        if(attendance is null)
        {
            return NotFound();
        }
        return Ok(attendance);
    }

    // POST api/<PracticeAttendancesController>
    [HttpPost]
    public async Task<ActionResult<PracticeAttendanceResponse>> AddAttendance(PracticeAttendanceRequest practiceAttendanceRequest)
    {
        var attendance = await _practiceAttendanceService.RegisterPracticeAttendanceAsync(practiceAttendanceRequest);
        if (attendance is null) { return BadRequest(); }

        return Ok(attendance);
    }

    // DELETE api/<PracticeAttendancesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<PracticeAttendanceResponse>> DeleteById([FromQuery] Guid id)
    {
        var deleteAttendance = await _practiceAttendanceService.DeleteByIdAsync(id);
        if(deleteAttendance is null) return BadRequest();

        return Ok(deleteAttendance);
    }
}
