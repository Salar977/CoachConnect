using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
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
        private readonly IMapper<Game, GameRegistrationDTO> _gameRegistrationMapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository,
                           IMapper<Game, GameDTO> gameMapper,
                           IMapper<Game, GameRegistrationDTO> gameRegistrationMapper,
                           ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
            _gameRegistrationMapper = gameRegistrationMapper;
            _logger = logger;
        }

        public async Task<GameRegistrationDTO?> CreateAsync(GameRegistrationDTO gameRegistrationDTO)
        {
            _logger.LogDebug("Create new Game");
            //Husk legge til sjekke om kampen finnes fra før dersom ikke så legge til ny kamp

            var gameExists = await _gameRepository.GetByExactGameTimeAsync(gameRegistrationDTO.GameTime);
            if (gameExists != null)
            {
                return null;
            }
            else
            {
                var game = _gameRegistrationMapper.MapToEntity(gameRegistrationDTO);
                game.Id = GameId.NewId;

                var res = await _gameRepository.CreateAsync(game);

                return res != null ? _gameRegistrationMapper.MapToDTO(res) : null;
            }
        }

        public async Task<GameDTO?> DeleteAsync(Guid id)
        {
            _logger.LogDebug("Deleting Game: {id}", id);

            var gameId = new GameId(id);
            var res = await _gameRepository.DeleteAsync(gameId);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

        public async Task<ICollection<GameDTO>> GetAllAsync(GameQuery gameQuery)
        {
            _logger.LogDebug("Getting all games");
            var res = await _gameRepository.GetAllAsync(gameQuery);
            return res.Select(game => _gameMapper.MapToDTO(game)).ToList();
        }

        //public Task<ICollection<GameDTO>> GetByGameTimeAsync(DateTime gameTime)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<GameDTO?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Getting Game by id: {id}", id);

            var gameId = new GameId(id);
            var res = await _gameRepository.GetByIdAsync(gameId);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

        //public Task<ICollection<GameDTO>> GetByLocationAsync(string location)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ICollection<GameDTO>> GetByOpponentNameAsync(string opponentName)
        //{
        //    _logger.LogDebug("Getting Game by opponent name: {opponentName}", opponentName);

        //    var res = await _gameRepository.GetByOpponentNameAsync(opponentName);
        //    var dtos = res.Select(game => _gameMapper.MapToDTO(game)).ToList();
        //    return dtos;
        //}

        public async Task<GameDTO?> UpdateAsync(Guid id, GameDTO gameDto)
        {
            _logger.LogDebug("Updating Game: {id}", id);

            var gameId = new GameId(id);
            var game = _gameMapper.MapToEntity(gameDto);
            game.Id = gameId;

            var res = await _gameRepository.UpdateAsync(gameId, game);
            return res != null ? _gameMapper.MapToDTO(game) : null;
        }
    }
    
}
