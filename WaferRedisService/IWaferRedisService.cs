using Models;
public interface IWaferRedisService
{
    
    public  Task<WaferRedis?> GetWaferReddis(string id);
    
}