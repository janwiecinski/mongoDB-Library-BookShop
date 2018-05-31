using MongoDB.Driver;

namespace DataAcces.DAL.Interfaces
{
    public interface IMyDatabaseWrapper
    {
       IMongoDatabase Database { get; }
    }
}
