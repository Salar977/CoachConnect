﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;

[Route("api/v1/[controller]")]
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

    // GET: api/v1/<UsersController>  // Husk endre alle disee til v1 og riktige linker
    [HttpGet(Name = "GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(int page = 1, int pageSize = 10)
    {
        _logger.LogDebug("Getting all users");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _userService.GetAllAsync(page, pageSize);
        return Ok(res);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // GET api/<UsersController>/
    [HttpGet("username", Name = "GetUserByUserName")]
    public async Task<ActionResult<UserDTO>> GetByUserName([FromQuery]string username)
    {
        _logger.LogDebug("Getting user by username: {userName}", username);

        var res = await _userService.GetByUserNameAsync(username);
        return res != null ? Ok(res) : BadRequest("Could not find any user with this username");
    }

    // GET api/<UsersController>/
    [HttpGet("lastname", Name = "GetUserByUserLastName")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserByUserLastName([FromQuery] string lastname)
    {
        _logger.LogDebug("Getting user by lastname: {lastName}", lastname);

        var res = await _userService.GetByUserLastNameAsync(lastname);
        return res != null ? Ok(res) : BadRequest("Could not find any users with this lastname");
    }

    // GET api/<UsersController>/
    [HttpGet("playername", Name = "GetUserByPlayerLastName")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserByPlayerLastName([FromQuery] string playername)
    {
        _logger.LogDebug("Getting user by playername: {playerName}", playername);

        var res = await _userService.GetByUserLastNameAsync(playername);
        return res != null ? Ok(res) : BadRequest("Could not find any users connected to this playername");
    }

    // POST api/<UsersController>
    [HttpPost ("register", Name = "RegisterUser") ]
    public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new user");

        var res = await _userService.RegisterUserAsync(dto);
        return res != null ? Ok(res) : BadRequest("Could not register new user");
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
