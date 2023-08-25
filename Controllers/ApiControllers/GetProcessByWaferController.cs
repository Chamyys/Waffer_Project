using AspNetCoreVueStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using Repository;
namespace Controllers;

[ApiController]
[Route("/api/proxy/[controller]/[action]")]
public class GetProcessByWaferController : Controller //postman example: http://192.168.11.10/public_api/process/waferId/1027
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly String _key;
    private readonly String _value;

    
    public GetProcessByWaferController (IHttpClientFactory httpClientFactory, IOptions<ApiKeyModel>  ApiKeyModel)
    {
        _httpClientFactory = httpClientFactory;
        _key = ApiKeyModel.Value.key;
        _value = ApiKeyModel.Value.value;
    }

    [HttpGet]
    public async Task<string> getProcessById(string number)
    {
        var pathData =  "http://192.168.11.10/public_api/process/waferId/" + number;
        var s = String.Empty;
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
           pathData)
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

