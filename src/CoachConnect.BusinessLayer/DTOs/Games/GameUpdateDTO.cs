using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.DTOs.Games;
public record GameUpdateDTO(

    string Location,
    TeamId HomeTeam,
    TeamId AwayTeam,
    DateTime GameTime
    );
