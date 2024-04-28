﻿using CoachConnect.BusinessLayer.DTOs.Games;
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
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper<Game, GameDTO> _gameMapper;
        private readonly IMapper<Game, GameUpdateDTO> _gameUpdateMapper;
        private readonly IMapper<Game, GameRegistrationDTO> _gameRegistrationMapper;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository,
                           IPracticeRepository practiceRepository,
                           ITeamRepository teamRepository,
                           IMapper<Game, GameDTO> gameMapper,
                           IMapper<Game, GameUpdateDTO> gameUpdateMapper,
                           IMapper<Game, GameRegistrationDTO> gameRegistrationMapper,
                           ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _practiceRepository = practiceRepository;
            _teamRepository = teamRepository;
            _gameMapper = gameMapper;
            _gameUpdateMapper = gameUpdateMapper;
            _gameRegistrationMapper = gameRegistrationMapper;
            _logger = logger;
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

        public async Task<GameUpdateDTO?> UpdateAsync(bool isAdmin, string idFromToken, Guid id, GameUpdateDTO gameUpdateDto)
        {
            _logger.LogDebug("Updating Game: {id}", id);

            DateTime startDate = gameUpdateDto.GameTime.Date;

            var practiceExists = await _practiceRepository.GetByPracticeTimeAsync(startDate);
            var gameExists = await _gameRepository.GetByGameTimeAsync(startDate);

            if (gameExists != null && gameExists.Any(game =>
                 game.AwayTeam.teamId == gameUpdateDto.AwayTeam.teamId ||
                 game.HomeTeam.teamId == gameUpdateDto.HomeTeam.teamId ||
                 game.AwayTeam.teamId == gameUpdateDto.HomeTeam.teamId ||
                 game.HomeTeam.teamId == gameUpdateDto.AwayTeam.teamId))
            {
                return null;
            }

            // Må også legge til sjekk om det finnes oppsatt trening på dato hvor kamp skal endres til           

            if (!isAdmin)
            {
                string idFromTokenBeforeExtraction = idFromToken;

                int startIndex = idFromTokenBeforeExtraction.IndexOf('=') + 1;
                int length = idFromTokenBeforeExtraction.IndexOf('}') - startIndex;
                string idFromTokenExtracted = idFromToken.Substring(startIndex, length);

                if (Guid.TryParse(idFromTokenExtracted, out var parsedCoachId))
                {
                    var coachId = new CoachId(parsedCoachId);

                    var homeTeamId = new TeamId(gameUpdateDto.HomeTeam.teamId);
                    var awayTeamId = new TeamId(gameUpdateDto.AwayTeam.teamId);

                    var homeTeam = await _teamRepository.GetByIdAsync(homeTeamId);
                    var awayTeam = await _teamRepository.GetByIdAsync(awayTeamId);

                    if (homeTeam == null || awayTeam == null)
                        return null; // custom ex

                    if (homeTeam.CoachId != coachId && awayTeam.CoachId != coachId)
                    {
                        return null; // custom ex
                    }
                }
            }

            var gameId = new GameId(id);
            var game = _gameUpdateMapper.MapToEntity(gameUpdateDto);
            game.Id = gameId;

            var res = await _gameRepository.UpdateAsync(gameId, game);
            return res != null ? _gameUpdateMapper.MapToDTO(game) : null;
        }

        public async Task<GameRegistrationDTO?> CreateAsync(bool isAdmin, string idFromToken, GameRegistrationDTO gameRegistrationDTO)
        {
            _logger.LogDebug("Create new Game");

            DateTime startDate = gameRegistrationDTO.GameTime.Date;

            var practiceExists = await _practiceRepository.GetByPracticeTimeAsync(startDate);
            var gameExists = await _gameRepository.GetByGameTimeAsync(startDate);

            if (gameExists != null && gameExists.Any(game =>
            game.AwayTeam.teamId == gameRegistrationDTO.AwayTeam.teamId ||
            game.HomeTeam.teamId == gameRegistrationDTO.HomeTeam.teamId ||
            game.AwayTeam.teamId == gameRegistrationDTO.HomeTeam.teamId ||
            game.HomeTeam.teamId == gameRegistrationDTO.AwayTeam.teamId))
            {
                return null; // custom ex
            }

            // Må også legge til sjekk om det finnes oppsatt trening på dato hvor kamp skal legges til         

            if (!isAdmin)
            {
                string idFromTokenBeforeExtraction = idFromToken;

                int startIndex = idFromTokenBeforeExtraction.IndexOf('=') + 1;
                int length = idFromTokenBeforeExtraction.IndexOf('}') - startIndex;
                string idFromTokenExtracted = idFromToken.Substring(startIndex, length);

                if (Guid.TryParse(idFromTokenExtracted, out var coachGuid))
                {
                    var coachId = new CoachId(coachGuid);
                    var homeTeamId = new TeamId(gameRegistrationDTO.HomeTeam.teamId);
                    var awayTeamId = new TeamId(gameRegistrationDTO.AwayTeam.teamId);

                    var homeTeam = await _teamRepository.GetByIdAsync(homeTeamId);
                    var awayTeam = await _teamRepository.GetByIdAsync(awayTeamId);

                    if (homeTeam == null || awayTeam == null)
                        return null;  // custom ex

                    if (coachId != homeTeam.CoachId && coachId != awayTeam.CoachId)
                    {
                        return null; // custom ex
                    }
                }
            }
            var game = _gameRegistrationMapper.MapToEntity(gameRegistrationDTO);
            game.Id = GameId.NewId;

            var res = await _gameRepository.CreateAsync(game);

            return res != null ? _gameRegistrationMapper.MapToDTO(res) : null;
        }

        public async Task<GameDTO?> DeleteAsync(bool isAdmin, string idFromToken, Guid id)
        {
            _logger.LogDebug("Deleting Game: {id}", id);
            
            if (!isAdmin)
            {
                string idFromTokenBeforeExtraction = idFromToken;

                int startIndex = idFromTokenBeforeExtraction.IndexOf('=') + 1;
                int length = idFromTokenBeforeExtraction.IndexOf('}') - startIndex;
                string idFromTokenExtracted = idFromToken.Substring(startIndex, length);

                if (Guid.TryParse(idFromTokenExtracted, out var coachGuid))
                {
                    var coachId = new CoachId(coachGuid);
                    var NewGameId = new GameId(id);

                    var game = await _gameRepository.GetByIdAsync(NewGameId);

                    if (game != null)
                    {
                        var homeTeamId = new TeamId(game.HomeTeam.teamId);
                        var awayTeamId = new TeamId(game.AwayTeam.teamId);

                        var homeTeam = await _teamRepository.GetByIdAsync(homeTeamId);
                        var awayTeam = await _teamRepository.GetByIdAsync(awayTeamId);

                        if (homeTeam == null || awayTeam == null)
                            return null; // custom ex

                        if (coachId != homeTeam.CoachId && coachId != awayTeam.CoachId)
                        {
                            return null; // custom ex
                        }
                    }
                }
            }

            var gameId = new GameId(id);
            var res = await _gameRepository.DeleteAsync(gameId);
            return res != null ? _gameMapper.MapToDTO(res) : null;
        }
    }
}
