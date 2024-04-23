using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.BusinessLayer.Mappers;
using Microsoft.OpenApi.Models;

namespace CoachConnect.API.Extensions;

public static class WebAppExtensions
{
    public static void RegisterMappers(this WebApplicationBuilder builder)
    {
        var assembly = typeof(UserMapper).Assembly;

        var mapperTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaceType = mapperType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            builder.Services.AddScoped(interfaceType, mapperType);
        }
    }
}

