using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services.Interfaces;
internal interface ICoachService
{
    Task<ICollection<CoachDTO>> GetAllAsync(QueryObject query);
}
