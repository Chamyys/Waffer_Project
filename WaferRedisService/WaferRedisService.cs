using Microsoft.AspNetCore.SpaServices;
using VueCliMiddleware;
using MongoDB.Driver;
using Repository;
using RabbitMQ.Client;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using RabbitRepository;
using Models;
using ChatHubSpace;



public class WaferRedisService : IWaferRedisService
{
    IDistributedCache cache;
    private IChatHub _chatHub;
     private readonly IMongoRepository<WaferRedis> _mongoRepository;
    public WaferRedisService(IDistributedCache distributedCache,IMongoRepository<WaferRedis> mongoRepository,IChatHub chatHub)
    {
        cache = distributedCache;
        _chatHub = chatHub;
        _mongoRepository = mongoRepository;
         _mongoRepository.setCollectionName("WaferRedis");
    }
    public async Task<WaferRedis?> SetWaferReddis(WaferRedis newWaferStatus)
    {
        WaferRedis? waferFromMongo = null;
       
         waferFromMongo = await _mongoRepository.GetById(newWaferStatus.id);
        if (waferFromMongo != null)
        {
            // если  найден, то добавляем в кэш
           
                Console.WriteLine($"{newWaferStatus.id} извлечен из базы данных");
                await cache.SetStringAsync(newWaferStatus.id.ToString(), newWaferStatus.currentStage); //закинуть в кэш
                Console.WriteLine(await cache.GetAsync(newWaferStatus.id.ToString()));
                _chatHub.Send($"{newWaferStatus.id} {newWaferStatus.currentStage}");
            
            // если нету в монге
        }
         
         
        else
        
            
         {  
         
                _mongoRepository.Create(newWaferStatus);
                 Console.WriteLine($"{newWaferStatus.id} Создан");
               await cache.SetStringAsync(newWaferStatus.id.ToString(), newWaferStatus.currentStage); //закинуть в кэш
                Console.WriteLine(await cache.GetAsync(newWaferStatus.id.ToString()));
                _chatHub.Send($"{newWaferStatus.id} {newWaferStatus.currentStage}");
        }
        
       
        return newWaferStatus;
    }
        
}