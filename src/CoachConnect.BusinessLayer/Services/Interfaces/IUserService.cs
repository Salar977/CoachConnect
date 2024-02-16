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
    Task<UserDTO?> GetByUserNameAsync(string userName);
    Task<UserDTO?> GetByLastNameAsync(string userLastName);
    Task<UserDTO?> GetByPlayerAsync(string playerLastName);
    Task<UserDTO?> UpdateAsync(int id, UserDTO dto, int loggedInUserId);
    Task<UserDTO?> DeleteAsync(int id, int loggedInUserId);  
    Task<int>? GetAuthenticatedIdAsync(string userName, string password);
    Task<UserDTO?> RegisterAsync(UserRegistrationDTO dto);
}