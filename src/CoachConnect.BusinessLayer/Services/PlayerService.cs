using CoachConnect.BusinessLayer.DTOs;
using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Services;
public class PlayerService : IPlayerService
{
    public Task<TeamDTO?> CreateAsync(TeamDTO teamDTO)
    {
        throw new NotImplementedException();
    }

    public Task<TeamDTO?> DeleteAsync(TeamId id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TeamDTO>> GetAllAsync(TeamQuery teamQuery)
    {
        throw new NotImplementedException();
    }

    public Task<TeamDTO?> GetByCoachIdAsync(CoachId coachid)
    {
        throw new NotImplementedException();
    }

    public Task<TeamDTO?> GetByIdAsync(TeamId id)
    {
        throw new NotImplementedException();
    }

    public Task<TeamDTO?> UpdateAsync(TeamId id, TeamDTO teamDto)
    {
        throw new NotImplementedException();
    }
}
