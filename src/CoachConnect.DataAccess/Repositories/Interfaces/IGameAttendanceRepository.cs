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
    //Task<UserDTO?> GetByIdAsync(UserId id);
    //Task<UserDTO?> UpdateAsync(UserId id, UserDTO dto);
    //Task<UserDTO?> DeleteAsync(UserId id);
    //Task<UserDTO?> RegisterGameAttendanceAsync(UserRegistrationDTO dto);
}
