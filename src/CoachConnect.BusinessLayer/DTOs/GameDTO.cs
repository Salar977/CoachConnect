using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs;

public record GameDTO(
    GameId Id,
    string Location,
    string OpponentName,
    DateTime GameTime,
    DateTime Created,
    DateTime Updated);
