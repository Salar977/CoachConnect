﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    /*
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger)
    {
        _playerService = playerService;
        _logger = logger;
    }

    public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetAllPlayers([FromQuery] PlayerQuery playerQuery)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _logger.LogDebug("Getting Players");

        return Ok(await _playerService.GetAllAsync(playerQuery));
    }

    [HttpGet("{id}", Name = "GetPlayerById")]
    public async Task<ActionResult<PlayerDTO>> GetPlayerById(Guid id)
    {
        _logger.LogDebug("Getting player by ID: {id}", id);

        var player = await _playerService.GetByIdAsync(new PlayerId(id));
        return player != null ? Ok(player) : NotFound($"Player with ID '{id}' not found");
    }

    [HttpPost("register", Name = "CreatePlayer")]
    public async Task<ActionResult<GameDTO>> CreatePlayer([FromBody] PlayerDTO playerDTO)
    {
        _logger.LogDebug("Create new Player");

        var res = await _playerService.CreateAsync(playerDTO);
        return res != null ? Ok(res) : BadRequest("Could not Create new player");
    }
    [HttpPut("{id}", Name = "UpdatePlayer")]
    public async Task<ActionResult<PlayerDTO>> UpdatePlayer(Guid id, [FromBody] PlayerDTO playerDTO)
    {
        _logger.LogDebug("Updating player with ID: {id}", id);

        var res = await _playerService.UpdateAsync(new PlayerId(id), playerDTO);
        return res != null ? Ok(res) : BadRequest("Could not update Player");
    }

    [HttpDelete("{id}", Name = "DeletePlayer")]
    public async Task<ActionResult<PlayerDTO>> DeletePlayer(Guid id)
    {
        _logger.LogDebug("Deleting game with ID: {id}", id);

        var res = await _playerService.DeleteAsync(new PlayerId(id));
        return res != null ? Ok(res) : BadRequest("Could not delete Player");
    }
    */

}
