using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using CoachConnect.DataAccess.Repositories.Interfaces;

namespace CoachConnect.BusinessLayer;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}