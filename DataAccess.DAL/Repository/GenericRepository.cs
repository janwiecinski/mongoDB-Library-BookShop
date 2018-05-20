using DataAcces.DAL.Interfaces;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcces.DAL
{
    public class GenericRepository<T>:IRepository<T> where T:BaseModel 
    {

        public IMongoCollection<T> Collection;
        public GenericRepository(IMyDatabase<T> database)
        {
            Collection = database.Collection;
        }

        public IEnumerable<T> GetItems()
        {
            var result = Collection.Find<T>(t=>true).ToList();
            return result;
        }

        public T GetItem(ObjectId id)
        {
            var result = Collection.Find<T>(t=>t.Id==id).FirstOrDefault();
            return result;
        }

        public T Create(T p)
        {
            p.Id = new ObjectId();

            Collection.InsertOne(p);

            return p;
        }

        public void Update( T entity)
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, entity.Id);
            Collection.ReplaceOne(filter, entity);
        }

        public void Remove(ObjectId id)
        {
            var searchedElement = Builders<T>.Filter.Eq(s => s.Id, id);
            Collection.DeleteOne(searchedElement);
        }
    }
}
