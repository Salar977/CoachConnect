﻿using CoachConnect.DataAccess.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.DTOs;

public record GameDTO(
    
    string Location,
    string OpponentName,
    DateTime GameTime,
    GameId Id);
