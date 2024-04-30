using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Players;
public record PlayerDTO
(
    string FirstName,
    string LastName,

    DateTime Created,
    DateTime Updated,
    UserId UserId,
    TeamId TeamId,
    PlayerId Id);