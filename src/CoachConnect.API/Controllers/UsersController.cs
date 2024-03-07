using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

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

    
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] UserQuery userQuery) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting users");

        return Ok(await _userService.GetAllAsync(userQuery));
    }

    // GET: https://localhost:7036/api/v1/users?page=1&pageSize=10
    //[HttpGet(Name = "GetUsers")]
    //public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] string? lastName, int page = 1, int pageSize = 10)
    //{
    //    _logger.LogDebug("Getting users");

    //    if (page < 1 || pageSize < 1 || pageSize > 50)
    //    {
    //        _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

    //        return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
    //    }

    //    var res = await _userService.GetAllAsync(lastName, page, pageSize);
    //    return res != null ? Ok(res) : BadRequest("Could not find any user with this criteria");
    //}

    // GET https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c
    [HttpGet("{id}", Name = "GetUserById")] 
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] Guid id) // bruk Guid her pga modelbinding kjenner ikke igjen vår custom UserId, så bruk Guid her og vi må konvertere under isteden
    {   
        _logger.LogDebug("Getting user by id {id}", id);
        
        var res = await _userService.GetByIdAsync(new UserId(id)); //var userId = new UserId(id); // Convert Guid to UserId her, da funker det
        return res != null ? Ok(res) : NotFound("Could not find any user with this id");        
    }

    // GET https://localhost:7036/api/v1/users/email?email=sara%40abc.no
    //[HttpGet("email", Name = "GetUserByEmail")]
    //public async Task<ActionResult<UserDTO>> GetUserByEmail([FromQuery] string email)
    //{
    //    _logger.LogDebug("Getting user by email: {email}", email);

    //    var res = await _userService.GetUserByEmailAsync(email);
    //    return res != null ? Ok(res) : BadRequest("Could not find any user with this email");
    //}

    // POST https://localhost:7036/api/v1/users/register
    [HttpPost ("register", Name = "RegisterUser") ]
    public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new user: {dto}", dto);

        var res = await _userService.RegisterUserAsync(dto);
        return res != null ? Ok(res) : BadRequest("Could not register new user");
    }

    // PUT https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c

    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO dto)
    {
        _logger.LogDebug("Updating user: {id}", id);

        var res = await _userService.UpdateAsync(new UserId(id), dto);
        return res != null ? Ok(res) : BadRequest("Could not update user");
    }

    // DELETE https://localhost:7036/api/v1/users/8f2466af-57c3-458c-82d8-676d80573c6c
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<ActionResult<UserDTO>> DeleteUser([FromRoute] Guid id)
    {
        _logger.LogDebug("Deleting user: {id}", id);

        var res = await _userService.DeleteAsync(new UserId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete user");
    }   
}