using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Games;
public record GameRegistrationDTO(

    string Location,
    string HomeTeam,
    string AwayTeam,
    DateTime GameTime);
