using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitRepository;
[ApiController]
[Route("/api/[controller]/[action]")]
public class RabbitMqController : Controller
{

     private  IWaferRedisService _waferRedisService;
     private  IRabbitMqConsumer _rabbitMqConsumer;
       public RabbitMqController(IWaferRedisService waferRedisService,IRabbitMqConsumer rabbitMqConsumer)//
    {
        _waferRedisService = waferRedisService;
        _rabbitMqConsumer = rabbitMqConsumer;
    }

    public void Consume()
    {
        //_rabbitMqConsumer("MyQueue","localhost").StartListening();   ///https://localhost:3000/api/RabbitMq/consume
        
       // new RabbitMqConsumer("MyQueue","localhost").StartListening();   ///https://localhost:3000/api/RabbitMq/consume
        _rabbitMqConsumer.setSettings("localhost", "MyQueue");
        _rabbitMqConsumer.StartListening();
        _waferRedisService.GetWaferReddis("42ea81cb");
   
    }

}