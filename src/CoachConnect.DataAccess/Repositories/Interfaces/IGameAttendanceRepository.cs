using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IGameAttendanceRepository
{
    Task<ICollection<GameAttendance>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery);
    Task<GameAttendance?> GetByIdAsync(GameAttendanceId id);
    Task<GameAttendance?> UpdateAsync(GameAttendanceId id, GameAttendance gameAttendance);
    Task<GameAttendance?> DeleteAsync(GameAttendanceId id);
    Task<GameAttendance?> RegisterGameAttendanceAsync(GameAttendance gameAttendance);
}
