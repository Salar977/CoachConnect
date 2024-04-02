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
        private readonly IPracticeRepository _practiceRepository;
        private readonly IMapper<Game, GameDTO> _gameMapper;
        private readonly IMapper<Game, GameRegistrationDTO> _gameRegistrationMapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository,
                           IPracticeRepository practiceRepository,
                           IMapper<Game, GameDTO> gameMapper,
                           IMapper<Game, GameRegistrationDTO> gameRegistrationMapper,
                           ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _practiceRepository = practiceRepository;
            _gameMapper = gameMapper;
            _gameRegistrationMapper = gameRegistrationMapper;
            _logger = logger;
        }

        public async Task<GameRegistrationDTO?> CreateAsync(GameRegistrationDTO gameRegistrationDTO)
        {
            _logger.LogDebug("Create new Game");

            // Get the start and end of the specified date
            DateTime startDate = gameRegistrationDTO.GameTime.Date;

            var gameExists = await _gameRepository.GetByGameTimeAsync(startDate);
            var practiceExists = await _practiceRepository.GetByPracticeTimeAsync(startDate);  
            if (gameExists != null || practiceExists != null)
            {
                return null; // denne returnerer bare null, bør returnere mld om at det allerede finnes game eller practice på denne dato, hm vanskelig
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

        public async Task<GameDTO?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Getting Game by id: {id}", id);

            var gameId = new GameId(id);
            var res = await _gameRepository.GetByIdAsync(gameId);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }

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
