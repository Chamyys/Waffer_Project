using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
namespace Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class WeatherForecastController : Controller
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

 
  private readonly IMongoRepository _mongoRepository;
    public WeatherForecastController(IMongoRepository mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] WELCOMEBACK entity)
{
    _mongoRepository.Create(entity);
}


  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    return Enumerable.Range(1, 50).Select(index => new WeatherForecast
    {
      Date = DateTime.Now.AddDays(index),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
}





/*
  [HttpGet]
  public List<WeatherForecast> Get()
  {
    List<WeatherForecast> list = new List<WeatherForecast>();
    for (int i = 0;i<50;i++){ list.Add(new WeatherForecast
    {
      Date = new DateTime(),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    });

    }
     return list; 
  }
}
*/