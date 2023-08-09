using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitRepository;
using ChatHubSpace;
[ApiController]
[Route("/api/[controller]/[action]")]
public class RabbitMqController : Controller
{

     private  IWaferRedisService _waferRedisService;
     private  RabbitMqConsumer _rabbitMqConsumer;
       public RabbitMqController(IWaferRedisService waferRedisService,RabbitMqConsumer rabbitMqConsumer)//
    {
        _waferRedisService = waferRedisService;
        _rabbitMqConsumer = rabbitMqConsumer;
    }

    public void Consume()
    {
      //  ChatHub chatHub = new ChatHub();
      //  chatHub.SendMessage("hello","Again");
        _rabbitMqConsumer.StartListening();  //https://localhost:3000/api/RabbitMq/consume    - старт consumerа

        _waferRedisService.GetWaferReddis("42ea81cb");
    } 

}