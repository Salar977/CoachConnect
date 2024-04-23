using CoachConnect.BusinessLayer.DTOs.Users;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.UserMappers;

public class UserUpdateMapper : IMapper<User, UserCoachUpdateDTO>
{
    public UserCoachUpdateDTO MapToDTO(User entity)
    {
        return new UserCoachUpdateDTO(entity.FirstName, entity.LastName, entity.PhoneNumber, entity.Email);
    }

    public User MapToEntity(UserCoachUpdateDTO dto)
    {
        return new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
        };
    }
}