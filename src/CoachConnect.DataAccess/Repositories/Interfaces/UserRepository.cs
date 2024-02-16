using CoachConnect.DataAccess.Entities;

namespace CoachConnect.DataAccess.Repositories.Interfaces;

public class UserRepository : IUserRepository
{
    public Task<User?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetAllAsync(int page, int pagesize)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByLastNameAsync(string userLastName)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByPlayerLastNameAsync(string playerLastName)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<User?> RegisterUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> UpdateAsync(int id, User user)
    {
        throw new NotImplementedException();
    }
}