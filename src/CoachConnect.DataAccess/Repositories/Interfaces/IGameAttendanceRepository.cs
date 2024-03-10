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
    //Task<GameAttendance?> GetByIdAsync(UserId id);
    //Task<GameAttendance?> UpdateAsync(UserId id, UserDTO dto);
    //Task<GameAttendance?> DeleteAsync(UserId id);
    Task<GameAttendance?> RegisterGameAttendanceAsync(GameAttendance gameAttendance);
}
