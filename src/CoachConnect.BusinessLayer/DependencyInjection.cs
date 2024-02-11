using CoachConnect.BusinessLayer.Services;
using CoachConnect.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoachConnect.BusinessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<SerilogTestService>();
        services.AddScoped<SerilogTestDataAccessLayer>();

        return services;
    }
}