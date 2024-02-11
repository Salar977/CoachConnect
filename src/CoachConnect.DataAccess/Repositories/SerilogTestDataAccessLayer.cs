using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;

public class SerilogTestDataAccessLayer
{
    private readonly ILogger<SerilogTestDataAccessLayer> _logger;

    public SerilogTestDataAccessLayer(ILogger<SerilogTestDataAccessLayer> logger)
    {
        _logger = logger;
        _logger.LogDebug("Testing logs DataAccessLayer");
    }
}