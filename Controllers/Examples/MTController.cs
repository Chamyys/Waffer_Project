using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class MTController : Controller{

  private readonly IMongoRepository<MT> _mongoRepository;
    public MTController(IMongoRepository<MT> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] MT entity)
{
    _mongoRepository.setCollectionName("MT");
    _mongoRepository.Create(entity);
}


 [HttpPost]
public void Delete([FromBody] MT entity)
{
    _mongoRepository.setCollectionName("MT");
      _mongoRepository.Delete(entity);
}


 [HttpPost]
public void Update([FromBody] MTController entity)
{
    _mongoRepository.setCollectionName("MT");
      _mongoRepository.Update(entity);
}
  [HttpGet]
  public  IList<MT>  Get()
  {
      _mongoRepository.setCollectionName("MT");
      return _mongoRepository.GetAll().Result;
  }
}