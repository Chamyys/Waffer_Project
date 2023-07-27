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

  private readonly IMongoRepository<WELCOMEBACK> _mongoRepository;
    public WeatherForecastController(IMongoRepository<WELCOMEBACK> mongoRepository)//
    {
        _mongoRepository = mongoRepository;
    }

 [HttpPost]
public void Post([FromBody] WELCOMEBACK entity)
{
    _mongoRepository.setCollectionName("WELCOMEBACK");
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


