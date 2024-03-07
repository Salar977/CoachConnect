using CoachConnect.DataAccess.Entities;
using CoachConnect.Shared.Helpers;

namespace CoachConnect.DataAccess.Repositories.Interfaces;
public interface IUserRepository
{
    // Task<ICollection<User>> GetAllAsync(int page, int pageSize);
    Task<ICollection<User>> GetAllAsync(UserQuery userQuery);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(string email);
    //Task<ICollection<User>> GetByLastNameAsync(string userLastName); // KAn vels lettes pga er innebygd i Getall??   
    Task<User?> UpdateAsync(UserId id, User user);
    Task<User?> DeleteAsync(UserId id);
    Task<User?> RegisterUserAsync(User user);
}