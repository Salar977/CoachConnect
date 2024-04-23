using CoachConnect.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs;
public record GameRegistrationDTO(

    string Location,
    string OpponentName,
    DateTime GameTime);
