using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs.Players;
public record PlayerRequest(string FirstName,
                            string LastName,
                            UserId UserId,
                            TeamId TeamId);