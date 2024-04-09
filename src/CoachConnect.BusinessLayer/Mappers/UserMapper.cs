using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User entity)
    {
        return new UserDTO()
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,            
            PhoneNumber = entity.PhoneNumber, 
            Email = entity.Email, 
            Id = entity.Id,
            Players = new List<PlayerDTO>(),
        };
    }

    public User MapToEntity(UserDTO dto) 
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