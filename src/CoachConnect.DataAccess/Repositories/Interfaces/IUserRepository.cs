using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IUserRepository
{
    // Task<ICollection<User>> GetAllAsync(int page, int pageSize);
    Task<ICollection<User>> GetAllAsync(QueryObject query);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<ICollection<User>> GetByLastNameAsync(string userLastName);
    Task<User?> RegisterUserAsync(User user);
    Task<User?> UpdateAsync(UserId id, User user);
    Task<User?> DeleteAsync(UserId id);
}