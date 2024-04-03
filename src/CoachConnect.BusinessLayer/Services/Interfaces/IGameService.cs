﻿using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IGameService
{
    Task<GameDTO?> GetByIdAsync(Guid id); 
    Task<ICollection<GameDTO>> GetAllAsync(GameQuery gameQuery); 
    Task<GameRegistrationDTO?> CreateAsync(GameRegistrationDTO gameRegistrationDTO); 
    Task<GameDTO?> UpdateAsync(Guid id, GameDTO gameDto); 
    Task<GameDTO?> DeleteAsync(Guid id); 
}
