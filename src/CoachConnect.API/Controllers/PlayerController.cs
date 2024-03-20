using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoachConnect.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    // GET: api/<PlayerController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<PlayerController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<PlayerController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<PlayerController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PlayerController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
