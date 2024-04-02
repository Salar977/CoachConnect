using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs;
public record GameAttendanceDTO(  
    
    // MÅ HA PLAYER NAME OGSÅ HER
    GameAttendanceId GameAttendanceId,
    GameId GameId,    
    PlayerId PlayerId);

