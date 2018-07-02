using DataAcces.DAL.Interfaces;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.DAL
{
    public class GenericRepository<T>:IRepository<T> where T:BaseModel 
    {
        public IMongoCollection<T> Collection;

        public GenericRepository(IMyDatabaseWrapper database)
        {
            Collection = database.Database.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetItems()
        {
            var result = Collection.Find<T>(t=>true).ToList();
            return result;
        }

        public T GetItem(ObjectId id)
        {
            var result = Collection.Find<T>(t=>t._Id.Equals(id)).FirstOrDefault();
            return result;
        }

        public T Create(T p)
        {
            if(p._Id.Equals(new ObjectId()))
            {
                p._Id = new ObjectId();
            }
            Collection.InsertOne(p);

            return p;
        }

        public void Update( T entity)
        {
            var filter = Builders<T>.Filter.Eq(s => s._Id, entity._Id);
            Collection.ReplaceOne(filter, entity);
        }

        public void Remove(ObjectId id)
        {
            var searchedElement = Builders<T>.Filter.Eq(s => s._Id, id);
            Collection.DeleteOne(searchedElement);
        }

        public void InsertMany(IEnumerable<T> entityList)
        {
            foreach (var item in entityList)
            {
                item._Id = new ObjectId();
            }
            try
            {
                Collection.InsertMany(entityList);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
