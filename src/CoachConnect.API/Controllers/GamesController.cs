﻿using CoachConnect.BusinessLayer.DTOs;
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

    //[HttpGet(Name = "GetAllGames")]
    //public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames(int page = 1, int pageSize = 10)
    //{
    //    _logger.LogDebug("Getting all games");

    //    if (page < 1 || pageSize < 1 || pageSize > 50)
    //    {
    //        _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);
    //        return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
    //    }

    //    var games = await _gameService.GetAllAsync(page, pageSize);
    //    return Ok(games);
    //}

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca808725555
    [HttpGet("{id}", Name = "GetGameById")]
    public async Task<ActionResult<GameDTO>> GetGameById(Guid id)
    {

        _logger.LogDebug("Getting game by ID: {id}", id);

        var game = await _gameService.GetByIdAsync(id);
        return game != null ? Ok(game) : NotFound($"Game with ID '{id}' not found");
    }

    //[HttpGet("opponent", Name = "GetGamesByOpponentName")]
    //public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByOpponentName([FromQuery] string opponentName)
    //{
    //    _logger.LogDebug("Getting games by opponent name: {opponentName}", opponentName);

    //    var games = await _gameService.GetByOpponentNameAsync(opponentName);
    //    return games != null ? Ok(games) : NotFound($"No games found with opponent name '{opponentName}'");
    //}

    // https://localhost:7036/api/v1/games/register
    [HttpPost("register", Name = "CreateGame")]
    public async Task<ActionResult<GameRegistrationDTO>> CreateGame([FromBody] GameRegistrationDTO gameRegistrationDTO)
    {
        _logger.LogDebug("Create new Game");

        var res = await _gameService.CreateAsync(gameRegistrationDTO);
        return res != null ? Ok(res) : BadRequest("Could not Create new game");
    }

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca80812cde3
    [HttpPut("{id}", Name = "UpdateGame")]
    public async Task<ActionResult<GameDTO>> UpdateGame(Guid id, [FromBody] GameDTO gameDTO)
    {
        _logger.LogDebug("Updating game with ID: {id}", id);

        var res = await _gameService.UpdateAsync(id, gameDTO);
        return res != null ? Ok(res) : BadRequest("Could not update Game");
    }

    // https://localhost:7036/api/v1/games/2f042e86-d75e-4591-a810-aca80812cde3
    [HttpDelete("{id}", Name = "DeleteGame")]
    public async Task<ActionResult<GameDTO>> DeleteGame(Guid id)
    {
        _logger.LogDebug("Deleting game with ID: {id}", id);

        var res = await _gameService.DeleteAsync(id);
        return res != null ? Ok(res) : BadRequest("Could not delete Game");
    }

}
