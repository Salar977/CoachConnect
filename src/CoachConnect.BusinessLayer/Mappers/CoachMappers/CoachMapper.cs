using CoachConnect.BusinessLayer.DTOs.Coach;
using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Coaches;

public class CoachMapper : IMapper<Coach, CoachDTO>
{
    public CoachDTO MapToDTO(Coach entity)
    {
        return new CoachDTO(entity.FirstName, entity.LastName, entity.PhoneNumber, entity.Email, entity.Id, new List<TeamResponse>()); // new List slik pga record anderledes syntax enn regular class
    }

    public Coach MapToEntity(CoachDTO dto) // Teams??
    {
        // var dtNow = DateTime.Now;
        return new Coach()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
        };
    }
}