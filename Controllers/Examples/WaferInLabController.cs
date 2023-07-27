using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class WaferInLabController : Controller
{


  private readonly IMongoRepository<WAFFERINLAB<WELCOMEBACK>> _mongoRepository;
    public WaferInLabController(IMongoRepository<WAFFERINLAB<WELCOMEBACK>> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] WAFFERINLAB<WELCOMEBACK> entity)
{
    _mongoRepository.setCollectionName("WAFFERINLAB");
    _mongoRepository.Create(entity);
}
  public  IList<WAFFERINLAB<WELCOMEBACK>>  Get()
  {
    _mongoRepository.setCollectionName("WAFFERINLAB");
     return _mongoRepository.GetAll().Result;
  }
}

