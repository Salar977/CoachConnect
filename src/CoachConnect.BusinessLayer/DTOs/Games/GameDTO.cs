using CoachConnect.DataAccess.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Games;

public record GameDTO(

    string Location,
    TeamId HomeTeam,
    TeamId AwayTeam,
    DateTime GameTime,
    GameId Id);
