using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IUserRepository
{
    Task<ICollection<User>> GetAllAsync(UserQuery userQuery);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> UpdateAsync(UserId id, User user);
    Task<User?> DeleteAsync(UserId id);
    Task<User?> RegisterUserAsync(User user);
}