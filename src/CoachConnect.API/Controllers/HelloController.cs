using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CoachConnect.API.Controllers;

[Route("api/v1/hello")]
[ApiController]
public class HelloController : ControllerBase
{
    [HttpGet(Name = "Hello")]
    public string Hello()
    {
        string hostName = System.Net.Dns.GetHostName();
        StringBuilder sb = new StringBuilder();
        foreach (var adr in System.Net.Dns.GetHostEntry(hostName).AddressList)
            sb.Append($"Adress: {adr.AddressFamily} {adr.ToString()}\n");
        return $"Hello from host: {hostName}\n{sb.ToString()}";
    }

}
