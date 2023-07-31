
using MongoDB.Bson;
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
            var collection = database.GetCollection<BsonDocument>(collectionData.Collection);
            
            foreach (var document in collectionData.Data)
            {
                collection.InsertOne(document);
            }
        }
    }
    
    private static List<SeedCollectionData> GetSeedData()
    {
        // Загрузите сид-данные из файла и верните их как список объектов SeedCollectionData
    }
}

public class SeedCollectionData
{
    public string Collection { get; set; }
    public List<BsonDocument> Data { get; set; }
}