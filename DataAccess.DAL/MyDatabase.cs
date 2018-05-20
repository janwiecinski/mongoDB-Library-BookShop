using DataAcces.DAL.Interfaces;
using MongoDB.Driver;

namespace DataAcces.DAL
{
    public class MyDatabase<T>: IMyDatabase<T>
    {
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<T> Collection { get; set; }
        public MyDatabase()
        {
           var _client = new MongoClient("mongodb://localhost:27017");
           Database = _client.GetDatabase("EmployeeDB");
           Collection = Database.GetCollection<T>(typeof(T).Name);
        }

     
    }
}
