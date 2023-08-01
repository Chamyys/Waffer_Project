using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]/[action]")]
[ApiController]
public class RabbitMqController : ControllerBase
{
	//private readonly IRabbitMqService _mqService;
    private readonly RabbitMqListener _listener;

	public RabbitMqController( IServiceProvider service)
	{
        _listener = service.GetService<RabbitMqListener>();

	}

/*
	[HttpPost]
	public void SendMessage([FromBody]string message)
	{
		_mqService.SendMessage(message);

		
	}
    */
    [HttpGet]
    public string GetMessage()
	{
        return _listener == null ?"null" : _listener._currentContent;
		//return _listener._currentContent;
	}
}