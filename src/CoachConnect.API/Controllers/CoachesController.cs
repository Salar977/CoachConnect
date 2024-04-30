using CoachConnect.BusinessLayer.DTOs.Coach;
using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin, Coach, User")]
    // GET: https://localhost:7036/api/v1/coaches
    [HttpGet(Name = "GetCoaches")]
    public async Task<ActionResult<IEnumerable<CoachDTO>>> GetCoaches([FromQuery] CoachQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting coaches");

        return Ok(await _coachService.GetAllAsync(query));
    }

    [Authorize(Roles = "Admin")]
    // GET https://localhost:7036/api/v1/coaches/2b1e02fc-4b92-4b0d-84a7-2418ff07ac13
    [HttpGet("{id}", Name = "GetCoachById")]
    public async Task<ActionResult<CoachDTO>> GetCoachById([FromRoute] Guid id)
    {
        _logger.LogDebug("Getting coach by id {id}", id);

        var res = await _coachService.GetByIdAsync(id); 
        return res != null ? Ok(res) : NotFound("Could not find any coach with this id");
    }

    [Authorize(Roles = "Admin , Coach")]
    // PUT https://localhost:7036/api/v1/coaches/92a93093-c123-4748-a8d9-558d61690d76
    [HttpPut("{id}", Name = "UpdateCoach")]
    public async Task<ActionResult<UserCoachUpdateDTO>> UpdateCoach(Guid id, [FromBody] UserCoachUpdateDTO dto)
    {
        _logger.LogDebug("Updating coach: {id}", id);

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        string idFromRoute = "CoachId { coachId = " + id.ToString() + " }";
        bool isAdmin = this.User.IsInRole("Admin");

        if (!isAdmin && !idFromToken.Equals(idFromRoute))
            return Unauthorized("No authorization to delete this coach");

        var res = await _coachService.UpdateAsync(id, dto);
        return res != null ? Ok(res) : BadRequest("Could not update coach");
    }

    [Authorize(Roles = "Admin , Coach")]
    // DELETE https://localhost:7036/api/v1/coaches/2b1e02fc-4b92-4b0d-84a7-2418ff07ac13
    [HttpDelete("{id}", Name = "DeleteCoach")]
    public async Task<ActionResult<CoachDTO>> DeleteCoach([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting coach: {id}", id);

        string idFromToken = (string)this.HttpContext.Items["UserId"]!;
        string idFromRoute = "CoachId { coachId = " + id.ToString() + " }";
        bool isAdmin = this.User.IsInRole("Admin");

        if (!isAdmin && !idFromToken.Equals(idFromRoute))
            return Unauthorized("No authorization to delete this coach");

        var res = await _coachService.DeleteAsync(id);
        return res != null ? Ok(res) : BadRequest("Could not delete coach");
    }

    [AllowAnonymous]
    // POST https://localhost:7036/api/v1/coaches/register
    [HttpPost("register", Name = "RegisterCoach")]
    public async Task<ActionResult<CoachDTO>> RegisterCoach([FromBody] CoachRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new coach: {email}", dto.Email);

        var res = await _coachService.RegisterCoachAsync(dto);
        return res != null ? Ok(res) : BadRequest("Could not register new coach");
    }
}
