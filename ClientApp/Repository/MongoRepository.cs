
using FluentResults;
using MongoDB.Driver;
using Models;
using MongoDB.Bson;

namespace Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : Entity  //Возможно придется менять на entity
    {

        private  IMongoCollection<T> _weathercollection;
        private IMongoDatabase _database;
        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            _database = database;
            var collection = database.GetCollection<T>(nameof(T));
            _weathercollection = collection;
        }

/*
public MongoRepository(IMongoClient client, string databaseName, string collectionName) конфигурация для нескольких таблиц
{
    var database = client.GetDatabase(databaseName);
    var collection = database.GetCollection<Entity>(collectionName);
    _weathercollection = collection;
}
*/      


        public void setCollectionName(string name){
        _weathercollection = _database.GetCollection<T>(name);
        }

        

        public async Task<string> Create(T car)
        {

            await _weathercollection.InsertOneAsync(car);
            return car.id;
        }

        public  Result Delete(T entity)
        {
            try
            { 
            var filter = Builders<T>.Filter.Eq(c => c.id, entity.id);
            var result =  _weathercollection.DeleteOneAsync(filter);
            return Result.Ok();
           // return result.DeletedCount == 1;
            }
            catch (Exception e)
            {
               return Result.Fail("No such element");
            }
          
        }

        public async Task<T> Get(T entity)
        {
            
                var filter = Builders<T>.Filter.Eq(c => c.id, entity.id);
                var car = _weathercollection.Find(filter).FirstOrDefaultAsync();
                if (car.Result.id == null) car.Result.id = "Not Found";
                return car.Result; 
        }



      public async Task Update(dynamic entity)
        {
        var filter = Builders<dynamic>.Filter.Eq("id", entity.id);
        var update = Builders<dynamic>.Update
        .Set("firstName", entity.firstName)
        .Set("secondName", entity.secondName)
        .Set("login", entity.login)
        .Set("password", entity.password)
        .Set("role", entity.role);
        await _weathercollection.UpdateOneAsync(filter, update);
        }




        public async Task<List<T>> GetAll() 
{
        var allEntities = await _weathercollection.Find(_ => true).ToListAsync();
        return allEntities;
}

    }
}