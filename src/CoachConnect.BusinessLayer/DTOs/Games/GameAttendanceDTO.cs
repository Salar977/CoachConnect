using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Games;
public record GameAttendanceDTO(

    string FirstName,
    string LastName,
    string OpponentName,
    DateTime GameTime,
    GameAttendanceId GameAttendanceId,
    GameId GameId,
    PlayerId PlayerId);

