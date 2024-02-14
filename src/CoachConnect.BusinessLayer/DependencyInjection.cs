using CoachConnect.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoachConnect.BusinessLayer;

public static class DependencyInjection
{

    public static IServiceCollection AddBLAndDAL(this IServiceCollection services)
    {

        services.AddScoped<DbConnection>();
        services.AddDbContext<CoachConnectDbContext>((serviceProivider, options) =>
        {
            var dbConnection = serviceProivider.GetRequiredService<DbConnection>();
            options.UseMySql(dbConnection.GetConnectionString(), ServerVersion.AutoDetect(dbConnection.GetConnectionString()));
        });


        return services;
    }
}