using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

[Route("api/[controller]")]
[ApiController]
public class RabbitMqController : ControllerBase

{

/*
	[Route("message/{message}")]
    [HttpGet]
    public dynamic GetMessage( [FromRoute] string message)
	{
        return message;
	}
    */
   
    [HttpGet]
    public string GetMessage( [FromBody] string message)
	{
        return message;
	}
}