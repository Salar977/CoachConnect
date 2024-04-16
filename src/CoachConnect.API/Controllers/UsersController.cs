using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;
    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }


    [Authorize(Roles = "Admin, Coach")]
    // GET: https://localhost:7036/api/v1/users
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] UserQuery userQuery) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting users");

        return Ok(await _userService.GetAllAsync(userQuery));
    }


    [Authorize(Roles = "Admin, Coach")]
    // GET https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c
    [HttpGet("{id}", Name = "GetUserById")] 
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] Guid id) // bruk Guid her pga modelbinding kjenner ikke igjen vår custom UserId, så bruk Guid her og vi må konvertere under isteden
    {   
        _logger.LogDebug("Getting user by id {id}", id);
        
        var res = await _userService.GetByIdAsync(id); 
        return res != null ? Ok(res) : NotFound("Could not find any user with this id");        
    }

    //[Authorize(Roles = "Admin, User")]
    // PUT https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c
    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO dto)
    {
        _logger.LogDebug("Updating user: {id}", id);

        var res = await _userService.UpdateAsync(id, dto);
        return res != null ? Ok(res) : BadRequest("Could not update user");
    }

    //[Authorize(Roles = "Admin, User")]
    // DELETE https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting user: {id}", id);

        var res = await _userService.DeleteAsync(id);
        return res != null ? Ok(res) : BadRequest("Could not delete user");
    }
        
    // POST https://localhost:7036/api/v1/users/register
    [HttpPost("register", Name = "RegisterUser")]
    public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new user: {email}", dto.Email);

        var res = await _userService.RegisterUserAsync(dto);
        return res != null ? Ok(res) : BadRequest("Could not register new user");
    }
}