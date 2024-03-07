using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories;
using CoachConnect.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper<Game, GameDTO> _gameMapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository,
                           IMapper<Game, GameDTO> gameMapper,
                           ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
            _logger = logger;
        }

        public async Task<GameDTO?> CreateAsync(GameDTO gameDTO)
        {
            _logger.LogDebug("Create new Game");
            //Husk legge til sjekke om kampen finnes fra før dersom ikke så legge til ny kamp

            var game = _gameMapper.MapToEntity(gameDTO);
            game.Id = GameId.NewId;

            var res = await _gameRepository.CreateAsync(game);

            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

        public async Task<GameDTO?> DeleteAsync(GameId id)
        {
            _logger.LogDebug("Deleting Game: {id}", id);

            var res = await _gameRepository.DeleteAsync(id);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

        public async Task<ICollection<GameDTO>> GetAllAsync(int page, int pageSize)
        {
            _logger.LogDebug("Getting all games");
            var res = await _gameRepository.GetAllAsync(page, pageSize);
            var dtos = res.Select(game => _gameMapper.MapToDTO(game)).ToList();
            return dtos;

        }

        public Task<ICollection<GameDTO>> GetByGameTimeAsync(DateTime gameTime)
        {
            throw new NotImplementedException();
        }

        public async Task<GameDTO?> GetByIdAsync(GameId id)
        {
            _logger.LogDebug("Getting Game by id: {id}", id);

            var res = await _gameRepository.GetByIdAsync(id);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

        public Task<ICollection<GameDTO>> GetByLocationAsync(string location)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<GameDTO>> GetByOpponentNameAsync(string opponentName)
        {
            _logger.LogDebug("Getting Game by opponent name: {opponentName}", opponentName);

            var res = await _gameRepository.GetByOpponentNameAsync(opponentName);
            var dtos = res.Select(game => _gameMapper.MapToDTO(game)).ToList();
            return dtos;
        }

        public async Task<GameDTO?> UpdateAsync(GameId id, GameDTO gameDto)
        {
            _logger.LogDebug("Updating Game: {id}", id);

            // husk at users (el admin) kun skal kunne eoppdatere sin egen user Dette må vel settes i JWT autorisering. Ikke glem må ha med dette viktig.
            // kanksje noe som : throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");

            var game = _gameMapper.MapToEntity(gameDto);
            game.Id = id;

            var res = await _gameRepository.UpdateAsync(id, game);
            return res != null ? _gameMapper.MapToDTO(game) : null;
        }
    }
    
}
