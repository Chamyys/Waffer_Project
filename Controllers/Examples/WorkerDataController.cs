using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class WorkerDataController : Controller{



 
  private readonly IMongoRepository<Worker> _mongoRepository;
    public WorkerDataController(IMongoRepository<Worker> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] Worker entity)
{
  
    _mongoRepository.setCollectionName(entity.role);
    _mongoRepository.Create(entity);
}

 [HttpPost]
public void Delete([FromBody] Worker entity)
{
      _mongoRepository.setCollectionName(entity.role);
      _mongoRepository.Delete(entity);
}


 [HttpPost]
public void Update([FromBody] Worker entity)
{
      _mongoRepository.setCollectionName(entity.role);
      _mongoRepository.Update(entity);
}
  [HttpGet]
  
  public  IList<Worker>  Get(string curentRole)
  {
    
     _mongoRepository.setCollectionName(curentRole);
     // return ((IList<Worker>)_mongoRepository.GetAllWorkersAsync(curentRole));
     return _mongoRepository.GetAll().Result;

  }
}