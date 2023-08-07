using Microsoft.AspNetCore.SpaServices;
using VueCliMiddleware;
using MongoDB.Driver;
using Repository;
using RabbitMQ.Client;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using RabbitRepository;
using Models;




public class WaferRedisService : IWaferRedisService
{
    IDistributedCache cache;
     private readonly IMongoRepository<WaferRedis> _mongoRepository;
    public WaferRedisService(IDistributedCache distributedCache,IMongoRepository<WaferRedis> mongoRepository)
    {
        cache = distributedCache;
        _mongoRepository = mongoRepository;
         _mongoRepository.setCollectionName("WaferRedis");
    }
    public async Task<WaferRedis?> GetWaferReddis(string id)
    {
        WaferRedis? wafer = null;
        var waferString = await cache.GetStringAsync(id.ToString());
        if (waferString != null) wafer = JsonSerializer.Deserialize<WaferRedis>(waferString);
 
        if (wafer == null)
        {
            wafer = await _mongoRepository.GetById(id);
            // если  найден, то добавляем в кэш
            if (wafer != null)  
            {
                Console.WriteLine($"{wafer.id} извлечен из базы данных");
                waferString = JsonSerializer.Serialize(wafer);
                await cache.SetStringAsync(wafer.id.ToString(), waferString); //закинуть в кэш
                Console.WriteLine(await cache.GetAsync(wafer.id.ToString()));
            }
        }
        else//если нету в монге
        {
            await cache.RemoveAsync(wafer.id.ToString()); //удалить
            Console.WriteLine($"{wafer.id} извлечен из кэша");
        }
        return wafer;
    }
}