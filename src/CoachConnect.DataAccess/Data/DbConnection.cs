using Microsoft.Extensions.Configuration;

namespace CoachConnect.DataAccess.Data;

public class DbConnection
{
    private readonly string? _connectionString;

    public DbConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("GokstadHageVennerDb");
        _connectionString = _connectionString?
                .Replace("{COACH_CONNECT_USERNAME}", Environment.GetEnvironmentVariable("COACH_CONNECT_USERNAME"))
                .Replace("{COACH_CONNECT_PASSWORD}", Environment.GetEnvironmentVariable("COACH_CONNECT_PASSWORD"));
    }

    public string GetConnectionString()
    {
        return _connectionString!;
    }
}