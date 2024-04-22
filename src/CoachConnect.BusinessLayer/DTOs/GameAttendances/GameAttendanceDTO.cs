using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.GameAttendances;
public record GameAttendanceDTO(

    string FirstName,
    string LastName,
    TeamId HomeTeam,
    TeamId AwayTeam,
    DateTime GameTime,
    GameAttendanceId GameAttendanceId,
    GameId GameId,
    PlayerId PlayerId);

