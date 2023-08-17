using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class MonitorConfigController : Controller{

  private readonly IMongoRepository<MonitorConfig> _mongoRepository;
    public MonitorConfigController(IMongoRepository<MonitorConfig> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody]MonitorConfig config)
{
    _mongoRepository.setCollectionName("MonitorConfig");
    _mongoRepository.Create(config);
}

  [HttpGet]
  
  public  IList<MonitorConfig>  Get()
  {
    
      _mongoRepository.setCollectionName("MonitorConfig");
      return _mongoRepository.GetAll().Result;
  }
   [HttpPost]
  public void Delete(MonitorConfig config){
        _mongoRepository.setCollectionName("MonitorConfig");
        _mongoRepository.Delete(config);
  }
}