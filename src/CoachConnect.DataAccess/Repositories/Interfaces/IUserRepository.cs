using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IUserRepository
{
    Task<ICollection<User>> GetAllAsync(string lastname, string email, string playerLastname, int page, int pageSize);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<ICollection<User>> GetByLastNameAsync(string userLastname);
    Task<ICollection<User>> GetByPlayerLastNameAsync(string playerLastname);
    Task<User?> RegisterUserAsync(User user);
    Task<User?> UpdateAsync(int id, User user);
    Task<User?> DeleteAsync(int id);
}