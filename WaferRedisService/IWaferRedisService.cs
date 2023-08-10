using Models;
public interface IWaferRedisService
{
    
    public  Task<WaferRedis?> SetWaferReddis(WaferRedis id);
    
}