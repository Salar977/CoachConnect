using Microsoft.AspNetCore.Mvc;

namespace CoachConnect.API.Controllers;
[Route("api/v1/coaches")]
[ApiController]
public class CoachesController : ControllerBase
{
    // GET: api/<CoachesController> // fyll inn disse
    [HttpGet(Name = "GetCoaches")]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<CoachesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CoachesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<CoachesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CoachesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
