using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class CoachUpdateMapper : IMapper<Coach, UserCoachUpdateDTO>
{
    public UserCoachUpdateDTO MapToDTO(Coach entity)
    {
        return new UserCoachUpdateDTO(entity.FirstName, entity.LastName, entity.PhoneNumber, entity.Email); 
    }

    public Coach MapToEntity(UserCoachUpdateDTO dto) 
    {
        return new Coach()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
        };
    }
}