using CoachConnect.BusinessLayer.DTOs.Teams;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Coach;

public record CoachDTO(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    CoachId Id,
    IEnumerable<TeamDTO> Teams);