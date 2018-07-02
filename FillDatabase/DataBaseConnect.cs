using MongoDB.Driver;

namespace FillDatabase
{
    public class DataBaseConnect
    {
        public IMongoDatabase Database { get; set; }
        public DataBaseConnect()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            Database = client.GetDatabase("BookLibrary");
       
        }
    }
}
