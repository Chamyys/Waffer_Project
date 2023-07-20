using MongoDB.Bson;
using MongoDB.Driver;
using Models;
using System.Net;
using FluentResults;

namespace Repository
{
public interface IMongoRepository
{
     public  Task<string> Create(Entity entity);
    public Result Delete(Entity entity);
    public Task<Entity> Get(Entity entity);
}
}

