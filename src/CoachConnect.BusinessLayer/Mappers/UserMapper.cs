using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User entity)
    {
        return new UserDTO(entity.Id, entity.UserName, entity.FirstName, entity.LastName, entity.PhoneNumber, entity.Email, entity.Created, entity.Updated);
    }

    public User MapToEntity(UserDTO dto)
    {
        var dtNow = DateTime.Now;
        return new User()
        {
            Id = dto.Id,
            UserName = dto.UserName,
            FirstName = dto.FirstName, 
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Created = dto.Created,
            Updated = dto.Updated,
        };
    }
}