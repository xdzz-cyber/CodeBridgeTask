using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet(Name = "Ping")]
    public string Ping()
    {
        return "Dogs house service. Version 1.0.1";
    }
}