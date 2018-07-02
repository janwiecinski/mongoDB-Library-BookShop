using DataAcces.DAL.Interfaces;
using MongoDB.Driver;

namespace DataAcces.DAL
{
    public class MyDatabaseWrapper : IMyDatabaseWrapper
    {
        public IMongoDatabase Database { get; set; }

        public MyDatabaseWrapper(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(database);
        }
    }
}
