using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers;

public class CoachRegistrationMapper : IMapper<Coach, CoachRegistrationDTO>
{
    public CoachRegistrationDTO MapToDTO(Coach entity)
    {
        throw new NotImplementedException();
    }

    public Coach MapToEntity(CoachRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Coach()
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