using CoachConnect.BusinessLayer.DTOs.Players;
using CoachConnect.BusinessLayer.DTOs.Teams;
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
    Task<PlayerDTO?> GetByIdAsync(Guid id);

    Task<ICollection<PlayerDTO>> GetAllAsync(PlayerQuery playerQuery);
    Task<ICollection<PlayerDTO?>> GetPlayersByTeamIdAsync(TeamId teamId);
    Task<ICollection<PlayerDTO?>> GetPlayersByUserIdAsync(UserId userId);

    Task<PlayerDTO?> CreateAsync(PlayerRequest playerReq);

    Task<PlayerDTO?> UpdateAsync(PlayerId id, PlayerUpdate playerupdate);

    Task<PlayerDTO?> DeleteAsync(PlayerId id);
}

