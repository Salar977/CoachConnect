using CoachConnect.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachConnect.DataAccess.Data;

public class CoachConnectDbContext : DbContext
{
    public CoachConnectDbContext(DbContextOptions<CoachConnectDbContext> options) : base(options)
    {
        
    }

}