using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IUserService
{
    // Task<ICollection<UserDTO>> GetAllAsync(string? lastName, int page, int pageSize);
    Task<ICollection<UserDTO>> GetAllAsync(QueryObject query);
    Task<UserDTO?> GetByIdAsync(UserId id);
    Task<UserDTO?> GetByEmailAsync(string email);
    Task<UserDTO?> UpdateAsync(UserId id, UserDTO dto);
    Task<UserDTO?> DeleteAsync(UserId id);     
    Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto);
}