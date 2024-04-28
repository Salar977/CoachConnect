using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoachConnect.DataAccess.Data;

public static class DataExtentions
{
    public static async void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CoachConnectDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}