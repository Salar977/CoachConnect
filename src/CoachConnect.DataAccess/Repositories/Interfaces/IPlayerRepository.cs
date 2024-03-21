using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IPlayerRepository
{
    Task<ICollection<Player>> GetAllAsync(PlayerQuery playerQuery);
    Task<Player?> GetByIdAsync(PlayerId id);
    Task<ICollection<Player?>> GetByUsersByIdAsync(UserId userId);
    Task<ICollection<Player?>> GetByTeamsIdAsync(TeamId teamId);
    Task<Player?> GetByPlayerFirstNameAsync(string player);
    Task<Player?> UpdateAsync(PlayerId id, Player player);
    Task<Player?> DeleteAsync(PlayerId id);
    Task<Player?> RegisterPlayerAsync(Player player);
}