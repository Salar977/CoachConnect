using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoachConnect.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CoachConnectDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameAttendanceRepository, GameAttendanceRepository>();
        services.AddScoped<ICoachRepository, CoachRepository>();
        services.AddScoped<IPracticeRepository, PracticeRepository>();

        return services;
    }
}