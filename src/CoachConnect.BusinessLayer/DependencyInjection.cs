using CoachConnect.BusinessLayer.Services.Interfaces;
using CoachConnect.BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CoachConnect.BusinessLayer;

public static class DependencyInjection
{

    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGameAttendanceService, GameAttendanceService>();
        services.AddScoped<ICoachService, CoachService>();
        services.AddValidatorsFromAssemblyContaining<CoachService>();
        services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);

        return services;
    }
}