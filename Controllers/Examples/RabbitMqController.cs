using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitRepository;
[ApiController]
[Route("/api/[controller]/[action]")]
public class RabbitMqController : ControllerBase
{

    public void Consume()
    {
        new RabbitMqConsumer("MyQueue","localhost").StartListening();
    }
}