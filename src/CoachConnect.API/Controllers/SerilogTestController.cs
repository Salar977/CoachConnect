using CoachConnect.BusinessLayer.Services;
using CoachConnect.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SerilogTestController : ControllerBase
{
    private readonly ILogger<SerilogTestController> _logger;
    private readonly SerilogTestService _testService;
    private readonly SerilogTestDataAccessLayer _testDataLayer;

    public SerilogTestController(ILogger<SerilogTestController> logger,
           SerilogTestService testService, 
           SerilogTestDataAccessLayer testDataLayer)
    {
        _logger = logger;            
        _testService = testService;
        _testDataLayer = testDataLayer;
    }

    // GET: api/<SerilogTestController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        _logger.LogDebug("Testing logs APILayer");
        return new string[] { "value1", "value2" };
    }

    // GET api/<SerilogTestController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SerilogTestController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SerilogTestController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SerilogTestController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
