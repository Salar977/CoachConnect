using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace CoachConnect.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Configuration
        services.AddDbContext<CoachConnectDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("CoachConnectDb");

            connectionString = connectionString?
                .Replace("{COACH_CONNECT_USERNAME}", Environment.GetEnvironmentVariable("COACH_CONNECT_USERNAME"))
                .Replace("{COACH_CONNECT_PASSWORD}", Environment.GetEnvironmentVariable("COACH_CONNECT_PASSWORD"));

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameAttendanceRepository, GameAttendanceRepository>();
        services.AddScoped<ICoachRepository, CoachRepository>();
        services.AddScoped<IPracticeRepository, PracticeRepository>();
        services.AddScoped<IPracticeAttendanceRepository, PracticeAttendanceRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        return services;
    }
}