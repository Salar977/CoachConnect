using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User entity)
    {
        return new UserDTO(entity.FirstName, entity.LastName, entity.PhoneNumber, entity.Email, entity.Id); // PLayers??
    }

    public User MapToEntity(UserDTO dto) // Players??
    {
        // var dtNow = DateTime.Now;
        return new User()
        {          
            FirstName = dto.FirstName, 
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,            
        };
    }
}