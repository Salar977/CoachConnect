using CoachConnect.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IUserService
{
    Task<ICollection<UserDTO>> GetAllAsync(int page, int pageSize);
    Task<UserDTO?> GetByIdAsync(int id);
    Task<UserDTO?> GetUserByEmailAsync(string email);
    Task<ICollection<UserDTO>> GetByUserLastNameAsync(string userLastname);
    Task<ICollection<UserDTO>> GetByPlayerLastNameAsync(string playerLastname);
    Task<UserDTO?> UpdateAsync(int id, UserDTO dto, int loggedInUserId);
    Task<UserDTO?> DeleteAsync(int id, int loggedInUserId);  
   // Task<int>? GetAuthenticatedIdAsync(string username, string password);
    Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto);
}