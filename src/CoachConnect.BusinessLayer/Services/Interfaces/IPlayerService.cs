using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IPlayerService
{
    Task<PlayerDTO?> GetByIdAsync(PlayerId id);
    Task<ICollection<PlayerDTO?>> GetPlayersByUserIdAsync(UserId userId);
    Task<ICollection<PlayerDTO?>> GetPlayersByTeamIdAsync(TeamId teamId);

    Task<ICollection<PlayerDTO>> GetAllAsync(PlayerQuery playerQuery);

    Task<PlayerDTO?> CreateAsync(PlayerDTO playerDTO);

    Task<PlayerDTO?> UpdateAsync(PlayerId id, PlayerDTO playerDto);

    Task<PlayerDTO?> DeleteAsync(PlayerId id);
}

