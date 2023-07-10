using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_vite_base_template.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class TestForecast : Controller
{


  [HttpGet]
  public string Get()
  {
    return "TEST";
  }
}
