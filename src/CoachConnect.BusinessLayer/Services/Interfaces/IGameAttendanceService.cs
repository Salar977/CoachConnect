using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IGameAttendanceService
{
    Task<ICollection<GameAttendanceDTO>> GetAllAsync(GameAttendanceQuery gameAttendanceQuery);
    //Task<GameAttendanceDTO?> GetByIdAsync(UserId id);
    //Task<GameAttendanceDTO?> UpdateAsync(UserId id, UserDTO dto);
    //Task<GameAttendanceDTO?> DeleteAsync(UserId id);
    Task<GameAttendanceDTO?> RegisterGameAttendanceAsync(GameAttendanceDTO dto);
}
