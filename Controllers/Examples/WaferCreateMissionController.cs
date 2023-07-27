using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class WaferCreateMissionController : Controller{



 
  private readonly IMongoRepository<WaferCreateMission> _mongoRepository;
    public WaferCreateMissionController(IMongoRepository<WaferCreateMission> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] WaferCreateMission entity)
{
  
    _mongoRepository.setCollectionName("Missions");
    _mongoRepository.Create(entity);
}

 [HttpPost]
public void Delete([FromBody] WaferCreateMission entity)
{
      _mongoRepository.setCollectionName(entity.type);
      _mongoRepository.Delete(entity);
}


 [HttpPost]
public void Update([FromBody] WaferCreateMission entity)
{
      _mongoRepository.setCollectionName(entity.type);
      _mongoRepository.Update(entity);
}
  [HttpGet]
  
  public  IList<WaferCreateMission>  Get()
  {
    
      _mongoRepository.setCollectionName("Missions");
      return _mongoRepository.GetAll().Result;
  }
}