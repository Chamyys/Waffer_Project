using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using Repository;
namespace Controllers;

[ApiController]
[Route("/api/proxy/[controller]")]
public class WaferController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly String _key;
    private readonly String _value;

    
    public WaferController(IHttpClientFactory httpClientFactory, IOptions<ApiKeyModel>  ApiKeyModel)//Все пластины с API
    {
        _httpClientFactory = httpClientFactory;
        _key = ApiKeyModel.Value.key;
        _value = ApiKeyModel.Value.value;
    }

    [HttpGet]
    [Route("all")]
    public async Task<string> GetAllWafers()
    {
        var s = String.Empty;
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "http://192.168.11.10/public_api/wafer/all")
        {
            Headers =
            {
                { _value, _key },
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            s = await httpResponseMessage.Content.ReadAsStringAsync();
        }
        return s;
    }
}

