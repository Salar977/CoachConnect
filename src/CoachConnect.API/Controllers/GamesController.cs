using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    [HttpGet(Name = "GetAllGames")]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames(int page = 1, int pageSize = 10)
    {
        _logger.LogDebug("Getting all games");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);
            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var games = await _gameService.GetAllAsync(page, pageSize);
        return Ok(games);
    }

    [HttpGet("{id}", Name = "GetGameById")]
    public async Task<ActionResult<GameDTO>> GetGameById(GameId id)
    {
        _logger.LogDebug("Getting game by ID: {id}", id);

        var game = await _gameService.GetByIdAsync(id);
        return game != null ? Ok(game) : NotFound($"Game with ID '{id}' not found");
    }

    [HttpGet("opponent", Name = "GetGamesByOpponentName")]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByOpponentName([FromQuery] string opponentName)
    {
        _logger.LogDebug("Getting games by opponent name: {opponentName}", opponentName);

        var games = await _gameService.GetByOpponentNameAsync(opponentName);
        return games != null ? Ok(games) : NotFound($"No games found with opponent name '{opponentName}'");
    }

    //[HttpPost(Name = "CreateGame")]
    //public async Task<ActionResult<GameDTO>> CreateGame([FromBody] GameDTO gameDTO)
    //{
    //    _logger.LogDebug("Creating new game");

    //    var createdGame = await _gameService.CreateAsync(gameDTO);
    //    return CreatedAtRoute("GetGameById", new { id = createdGame.Id }, createdGame);
    //}

    //[HttpPut("{id}", Name = "UpdateGame")]
    //public async Task<IActionResult> UpdateGame(GameId id, [FromBody] GameDTO gameDTO)
    //{
    //    _logger.LogDebug("Updating game with ID: {id}", id);

        //    try
        //    {
        //        var updatedGame = await _gameService.UpdateAsync(id, gameDTO);
        //        return Ok(updatedGame);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound($"Game with ID '{id}' not found");
        //    }
        //}

        [HttpDelete("{id}", Name = "DeleteGame")]
    public async Task<IActionResult> DeleteGame(GameId id)
    {
        _logger.LogDebug("Deleting game with ID: {id}", id);

        var deleted = await _gameService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound($"Game with ID '{id}' not found");
    }
}
