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


   [HttpGet]
  public  IList<WAFFERINLAB<WELCOMEBACK> >  Get( )
  {

    _mongoRepository.setCollectionName("WAFFERINLAB");
     // return ((IList<Worker>)_mongoRepository.GetAllWorkersAsync(curentRole));
     return _mongoRepository.GetAll().Result;
  }
 [HttpPost]
public void Post([FromBody] WAFFERINLAB<WELCOMEBACK> entity)
{
    _mongoRepository.setCollectionName("WAFFERINLAB");
    _mongoRepository.Create(entity);
}
  public  WAFFERINLAB<WELCOMEBACK>  GetById(string id)
  {
    _mongoRepository.setCollectionName("WAFFERINLAB");
    var a = _mongoRepository.GetById(id).Result;
     return  a;
  }
}

