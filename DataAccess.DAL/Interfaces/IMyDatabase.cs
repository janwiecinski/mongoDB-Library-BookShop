using MongoDB.Driver;

namespace DataAcces.DAL.Interfaces
{
    public interface IMyDatabase<T>
    {
       IMongoDatabase Database { get; }
       IMongoCollection<T> Collection { get; }
    }
}
