using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs;
public record TeamDTO
(
    string TeamCity,
    string TeamName,
    DateTime Created,
    DateTime Updated,
    CoachId CoachId,
    TeamId Id);