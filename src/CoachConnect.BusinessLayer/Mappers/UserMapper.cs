using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User entity)
    {
        return new UserDTO(entity.FirstName, entity.LastName, new List<PlayerDTO>(), entity.PhoneNumber, entity.Email, entity.Id);
        //return new UserDTO(entity.FirstName, entity.LastName, (IEnumerable<PlayerDTO>)entity.Players, entity.PhoneNumber, entity.Email, entity.Id); // PLayers??
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