using CoachConnect.BusinessLayer.DTOs.Games;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;

[Route("api/v1/games")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ILogger<GamesController> _logger;

    public GamesController(IGameService gameService, ILogger<GamesController> logger)
    {
        _gameService = gameService;
        _logger = logger;
    }

    // https://localhost:7036/api/v1/games
    [HttpGet(Name = "GetAllGames")]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames([FromQuery] GameQuery gameQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Games");

        return Ok(await _gameService.GetAllAsync(gameQuery));
    }

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca808725555
    [HttpGet("{id}", Name = "GetGameById")]
    public async Task<ActionResult<GameDTO>> GetGameById(Guid id)
    {
        _logger.LogDebug("Getting game by ID: {id}", id);

        var game = await _gameService.GetByIdAsync(id);
        return game != null ? Ok(game) : NotFound($"Game with ID '{id}' not found");
    } 

    // https://localhost:7036/api/v1/games/register
    [HttpPost("register", Name = "CreateGame")]
    public async Task<ActionResult<GameRegistrationDTO>> CreateGame([FromBody] GameRegistrationDTO gameRegistrationDTO)
    {
        _logger.LogDebug("Create new Game");

        var res = await _gameService.CreateAsync(gameRegistrationDTO);
        return res != null ? Ok(res) : BadRequest("Could not create new game");
    }

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca80812cde3
    [HttpPut("{id}", Name = "UpdateGame")]
    public async Task<ActionResult<GameUpdateDTO>> UpdateGame(Guid id, [FromBody] GameUpdateDTO gameUpdateDTO)
    {
        _logger.LogDebug("Updating game with ID: {id}", id);

        var res = await _gameService.UpdateAsync(id, gameUpdateDTO);
        return res != null ? Ok(res) : BadRequest("Could not update game");
    }

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca80812cde3
    [HttpDelete("{id}", Name = "DeleteGame")]
    public async Task<ActionResult<GameDTO>> DeleteGame(Guid id)
    {
        _logger.LogDebug("Deleting game with ID: {id}", id);

        var res = await _gameService.DeleteAsync(id);
        return res != null ? Ok(res) : BadRequest("Could not delete game");
    }

}
