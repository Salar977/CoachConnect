using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
public interface IUserService
{
    Task<ICollection<UserDTO>> GetAllAsync(string? lastName, int page, int pageSize);
    Task<UserDTO?> GetByIdAsync(UserId id);
    Task<UserDTO?> GetUserByEmailAsync(string email);
    Task<UserDTO?> UpdateAsync(UserId id, UserDTO dto);
    Task<UserDTO?> DeleteAsync(UserId id);  
   // Task<int>? GetAuthenticatedIdAsync(string username, string password);
    Task<UserDTO?> RegisterUserAsync(UserRegistrationDTO dto);
}