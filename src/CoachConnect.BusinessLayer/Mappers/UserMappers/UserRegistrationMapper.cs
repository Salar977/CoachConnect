using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.UserMappers;

public class UserRegistrationMapper : IMapper<User, UserRegistrationDTO>
{
    public UserRegistrationDTO MapToDTO(User entity)
    {
        throw new NotImplementedException();
    }

    public User MapToEntity(UserRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Created = dtNow,
            Updated = dtNow,
        };
    }
}