using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]/[action]")]
[ApiController]
public class RabbitMqController : ControllerBase
{
	//private readonly IRabbitMqService _mqService;
    private readonly RabbitMqListener _listener;



/*
	[HttpPost]
	public void SendMessage([FromBody]string message)
	{
		_mqService.SendMessage(message);

		
	}
    */
    [HttpGet]
    public string GetMessage([FromBody] string message)
	{
        return message;
}
}