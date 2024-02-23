using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IUserRepository
{
    Task<ICollection<User>> GetAllAsync(int page, int pageSize);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<ICollection<User>> GetByLastNameAsync(string userLastName);
    Task<User?> RegisterUserAsync(User user);
    Task<User?> UpdateAsync(UserId id, User user);
    Task<User?> DeleteAsync(UserId id);
}