using CoachConnect.BusinessLayer.DTOs.GameAttendances;
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
    Task<GameAttendanceRegistrationDTO?> RegisterGameAttendanceAsync(bool isAdmin, string idFromToken, GameAttendanceRegistrationDTO dto);
    Task<GameAttendanceDTO?> DeleteAsync(bool isAdmin, string idFromToken, Guid id);
}
