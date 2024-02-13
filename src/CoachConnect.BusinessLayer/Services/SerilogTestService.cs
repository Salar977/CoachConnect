using Microsoft.Extensions.Logging;

namespace CoachConnect.BusinessLayer.Services;

public class SerilogTestService
{
    private readonly ILogger _logger;

    public SerilogTestService(ILogger<SerilogTestService> logger)
    {
        _logger = logger;
        _logger.LogDebug("Testing logs BusinessLayer");
    }
}