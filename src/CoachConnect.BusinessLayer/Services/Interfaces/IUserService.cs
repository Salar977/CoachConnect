using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IUserService
{
    // Task<ICollection<UserDTO>> GetAllAsync(string? lastName, int page, int pageSize);
    Task<ICollection<UserDTO>> GetAllAsync(UserQuery userQuery);
    Task<UserDTO?> GetByIdAsync(Guid id);
    Task<UserDTO?> GetByEmailAsync(string email);
    Task<UserCoachUpdateDTO?> UpdateAsync(Guid id, UserCoachUpdateDTO dto);
    Task<UserDTO?> DeleteAsync(Guid id);     
    Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto);
}