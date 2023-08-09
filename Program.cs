using Microsoft.AspNetCore.SpaServices;
using VueCliMiddleware;
using MongoDB.Driver;
using Repository;
using RabbitMQ.Client;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using RabbitRepository;
using Models;
using ChatHubSpace;
using SignalRService;
using Microsoft.AspNet;
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.





  builder.Services.AddSingleton<RabbitMqConsumer>(provider => new RabbitMqConsumer("localhost","MyQueue"));

/* 
builder.Services.AddSingleton<IMongoRepository>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = "YourDatabaseName"; конфигурация для нескольких таблиц
    var collectionName = "YourCollectionName";
    return new MongoRepository(client, databaseName, collectionName);
});
*/ 
  builder.Services.AddStackExchangeRedisCache(options => { //редис
    options.Configuration = "localhost";
    options.InstanceName = "wafer_id_in_redis-";
});


builder.Services.AddTransient<IWaferRedisService,WaferRedisService>();
                                                   //builder.Services.AddSingleton<IRabbitMqConsumer,RabbitMqConsumer>();

  builder.Services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
  builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("Mongo")));





/*
Регистрация Монги на 1 тип репозитория без T
builder.Services.AddTransient<IMongoRepository, MongoRepository>();
builder.Services.Configure<IMongoRepository>(builder.Configuration.GetSection("Mongo")); 
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("Mongo")));


*/ // builder.Services.AddSingleton<IRabbitMqListener>();
  //builder.Services.AddTransient(typeof(IRabbitMqListener), typeof(RabbitMqListener));


 //builder.Services.AddSingleton<IRabbitMqListener, RabbitMqListener>();

         //   builder.Services.AddHostedService<RabbitMqListener>(); //Рабочий Вариант с листенером из мануала


      /*Подключение рэббит мку не рабочее

           builder.Services.AddRabbitMqClient(clientConfiguration);
          builder.Services.AddExchange("ExchangeName", isConsuming: true, exchangeConfiguration);
    

    services.AddSingleton<IHostedService, ConsumingService>();

*/
  // We will use different credentials for connections.
    




//Seeder.Seed(builder.Configuration.GetConnectionString("Mongo"),"Test_Mongo"); Раскоментить для копирования Бд



 builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => 
            {
                builder.AllowCredentials()
                .WithOrigins(
                    "https://localhost:3000/","https://localhost:7001/","/","localhost")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });




 builder.Services.AddSignalR();





builder.Services.AddControllersWithViews();


builder.Services.AddSpaStaticFiles(configuration =>
{
  configuration.RootPath = "ClientApp/dist";
});

var app = builder.Build();

 app.UseCors("AllowAllOrigins");

/*
app.UseCors(builder =>
    builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);
*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseHttpsRedirection();
}
else
{
  app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseSpaStaticFiles();






app.UseRouting();

app.UseEndpoints(endpoints =>
{
endpoints.MapHub<ChatHub>("/chat");
});

        
    

/*
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets();
*/
/*
app.MapGet("/user/{id}", async (string id, WorkerService workerService) =>    //рэдис
{
    Worker? user = await workerService.GetUser(id);
    if (user != null) return $"User {user.firstName}  Id={user.secondName}  Age={user.id}";
    return "User not found";
});
*/

app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
  app.MapToVueCliProxy(
      "{*path}",
      new SpaOptions { SourcePath = "ClientApp" },
      npmScript: "serve",
      regex: "Compiled successfully!");
}


app.UseSpa(spa =>
{
  spa.Options.SourcePath = "ClientApp";
});

app.MapFallbackToFile("index.html"); ;// все запросы проксируются тут 


      //  WebSocketServer server = new WebSocketServer();
       // server.Start("http://localhost:9000/");
       // Console.WriteLine("WebSocket сервер запущен. Нажмите любую клавишу, чтобы остановить сервер.");
        //Console.ReadLine();
        //server.Stop();

app.Run();
