using Microsoft.AspNetCore.SpaServices;
using VueCliMiddleware;
using MongoDB.Driver;
using Repository;
using RabbitMQ.Client;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using RabbitRepository;
using Models;
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
          builder =>
          {
            // To add custom origin sources replace AllowAnyOrigin with:

            // WithOrigin("{{YOUR_CUSTOM_URL}}")

            // You can also have multiple origins within this section, seperate with a comma
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
          }
      );
});





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
builder.Services.AddSingleton<IRabbitMqConsumer,RabbitMqConsumer>();

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






builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSpaStaticFiles(configuration =>
{
  configuration.RootPath = "ClientApp/dist";
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

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
app.UseWebSockets();
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

app.MapRazorPages();

app.UseSpa(spa =>
{
  spa.Options.SourcePath = "ClientApp";
});

app.MapFallbackToFile("index.html"); ;// все запросы проксируются тут 








app.Run();
