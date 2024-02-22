using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
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

    // GET: api/v1/<UsersController>  // Husk endre alle disee til v1 og riktige linker
    [HttpGet(Name = "GetUsersBy")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersBy([FromQuery]string? lastName, int page = 1, int pageSize = 10) // husk legge til sortere alfabetisk også på Coach
    {
        _logger.LogDebug("Getting users");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _userService.GetAllAsync(lastName, page, pageSize);
        return res != null ? Ok(res) : BadRequest("Could not find any user with this criteria");
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}", Name = "GetUserById")] 
    public string Get(string id)
    {
        return "value";
    }

    //// GET api/<UsersController>/
    //[HttpGet("email", Name = "GetUserByEmail")]
    //public async Task<ActionResult<UserDTO>> GetUserByEmail([FromQuery] string email)
    //{
    //    _logger.LogDebug("Getting user by email: {email}", email);

    //    var res = await _userService.GetUserByEmailAsync(email);
    //    return res != null ? Ok(res) : BadRequest("Could not find any user with this email");
    //}

    //// GET api/<UsersController>/
    //[HttpGet("lastname", Name = "GetUserByUserLastName")] // DENNE FOR QUERY?
    //public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserByUserLastName([FromQuery] string lastName)
    //{
    //    _logger.LogDebug("Getting user by lastname: {lastName}", lastName);

    //    var res = await _userService.GetByUserLastNameAsync(lastName);
    //    return res != null ? Ok(res) : BadRequest("Could not find any users with this lastname");
    //}

    //// GET api/<UsersController>/
    //[HttpGet("playername", Name = "GetUserByPlayerLastName")] // DENNE FOR QUERY?
    //public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserByPlayerLastName([FromQuery] string playerName)
    //{
    //    _logger.LogDebug("Getting user by playername: {playerName}", playerName);

    //    var res = await _userService.GetByUserLastNameAsync(playerName);
    //    return res != null ? Ok(res) : BadRequest("Could not find any users connected to this playername");
    //}

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
