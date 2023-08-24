
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
public class Seeder
{
    public static void Seed(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName); 
        
        var seedData = GetSeedData();
        
        foreach (var collectionData in seedData)
        {
            var collection = database.GetCollection<BsonDocument>(collectionData.collection);
            
            foreach (var document in collectionData.data)
            {
                collection.InsertOne(document);
            }
        }
    }
    
    private static List<SeedCollectionData> GetSeedData()
    {         
            string json = File.ReadAllText("/home/egor/WAFERPATROL/mongoGetStarted.json"); //поменять коннект при необходимости
           var documents = BsonSerializer.Deserialize<List<SeedCollectionData>>(json);
            return documents;
    }
}

public class SeedCollectionData
{
    public string collection { get; set; }
    public List<BsonDocument> data { get; set; }
}