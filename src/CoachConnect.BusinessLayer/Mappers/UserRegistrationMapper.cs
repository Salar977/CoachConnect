using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

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
            Username = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Created = dtNow,
            Updated = dtNow,
        };        
    }
}