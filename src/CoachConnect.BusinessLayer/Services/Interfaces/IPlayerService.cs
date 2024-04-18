using CoachConnect.BusinessLayer.DTOs;
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
    Task<ICollection<PlayerDTO?>> GetByTeamIdAsync(TeamId teamid);
    Task<ICollection<PlayerDTO?>> GetByUserIdAsync(UserId userid);


    Task<ICollection<PlayerDTO>> GetAllAsync(PlayerQuery playerQuery);

    Task<PlayerDTO?> CreateAsync(PlayerRequest playerRequest);

    Task<PlayerDTO?> UpdateAsync(PlayerId id, PlayerDTO playerDto);

    Task<PlayerDTO?> DeleteAsync(PlayerId id);
}

