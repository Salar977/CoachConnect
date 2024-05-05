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
    Task<PlayerResponse?> GetByIdAsync(Guid id);

    Task<ICollection<PlayerResponse>> GetAllAsync(PlayerQuery playerQuery);
    Task<ICollection<PlayerResponse?>> GetPlayersByTeamIdAsync(TeamId teamId);
    Task<ICollection<PlayerResponse?>> GetPlayersByUserIdAsync(UserId userId);

    Task<PlayerResponse?> CreateAsync(PlayerRequest playerReq);

    Task<PlayerResponse?> UpdateAsync(PlayerId id, PlayerUpdate playerupdate);

    Task<PlayerResponse?> DeleteAsync(PlayerId id);
}

