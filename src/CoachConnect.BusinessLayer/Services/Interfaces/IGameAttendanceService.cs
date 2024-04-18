using CoachConnect.BusinessLayer.DTOs.Games;
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
    Task<GameAttendanceDTO?> GetByIdAsync(Guid id);

    //Task<GameAttendanceDTO?> UpdateAsync(Guid id, GameAttendanceDTO dto);
    Task<GameAttendanceDTO?> DeleteAsync(Guid id);
    Task<GameAttendanceRegistrationDTO?> RegisterGameAttendanceAsync(GameAttendanceRegistrationDTO dto);
}
