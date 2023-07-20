
using FluentResults;
using MongoDB.Driver;
using Models;
namespace Repository
{
    public class MongoRepository : IMongoRepository
    {

        private readonly IMongoCollection<Entity> _weathercollection;

        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Mongo");
            var collection = database.GetCollection<Entity>(nameof(Entity));
            _weathercollection = collection;
        }

        public async Task<string> Create(Entity car)
        {
            
                _weathercollection.InsertOneAsync(car);
                   return car.id;
             
        }

        public  Result Delete(Entity entity)
        {
            try
            { 
            var filter = Builders<Entity>.Filter.Eq(c => c.id, entity.id);
            var result =  _weathercollection.DeleteOneAsync(filter);
            return Result.Ok();
           // return result.DeletedCount == 1;
            }
            catch (Exception e)
            {
               return Result.Fail("No such element");
            }
          
        }

        public async Task<Entity> Get(Entity entity)
        {
            
                var filter = Builders<Entity>.Filter.Eq(c => c.id, entity.id);
                var car = _weathercollection.Find(filter).FirstOrDefaultAsync();
                if (car.Result.id == null) car.Result.id = "Not Found";
                return car.Result;
            
           
            
        }
/*
    public async Task<IEnumerable<Car>> Get()
    {
        var cars = await _cars.Find(_ => true).ToListAsync();

        return cars;
    }

    public async Task<IEnumerable<Car>> GetByMake(string make)
    {
        var filter = Builders<Car>.Filter.Eq(c => c.Make, make);
        var cars = await _cars.Find(filter).ToListAsync();

        return cars;
    }

    public async Task<bool> Update(ObjectId objectId, Car car)
    {
        var filter = Builders<Car>.Filter.Eq(c => c.Id, objectId);
        var update = Builders<Car>.Update
            .Set(c => c.Make, car.Make)
            .Set(c => c.Model, car.Model)
            .Set(c => c.TopSpeed, car.TopSpeed);
        var result = await _cars.UpdateOneAsync(filter, update);

        return result.ModifiedCount == 1;
    }
*/
    }
}