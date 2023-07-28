using MongoDB.Bson;
using MongoDB.Driver;
using Models;
using System.Net;
using FluentResults;

namespace Repository{

public interface IMongoRepository<T> where T : Entity
{
    public void setCollectionName(string name);
    public Task<string> Create(T car);
    public Result Delete(T entity);
    public Task<T> Get(T entity);
    public Task<T> GetById(string Id);
    public Task<List<T>> GetAll();
       public  Task Update(dynamic entity);
}
}
