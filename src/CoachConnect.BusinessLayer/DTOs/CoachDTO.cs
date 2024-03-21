using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;

public record CoachDTO(
    string FirstName,
    string LastName,
    //IEnumerable<TeamDTO> Teams,
    string PhoneNumber,    
    string Email,
    CoachId Id);

