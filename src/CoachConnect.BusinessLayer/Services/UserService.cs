using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;

namespace CoachConnect.BusinessLayer.Services;

public class UserService : IUserService
{

    public Task<UserDTO> DeleteUserAsync(int id, int loggedInUserId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserDTO>> GetAllUsersAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetAuthenticatedIdAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetByLastNameAsync(string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetByPlayerAsync(string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> RegisterAsync(UserRegistrationDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> UpdateUserAsync(int id, UserDTO dto, int loggedInUserId)
    {
        throw new NotImplementedException();
    }
}