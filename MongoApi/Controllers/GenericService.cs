using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoApi.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MongoApi.Controllers
{
    public class GenericService<T>: IGenericService<T> where T : BaseModel
    {
        private readonly IRepository<T> _repository;

        public GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetItems()
        {
            return _repository.GetItems();
        }
        public T GetItem(ObjectId id)
        {
           return _repository.GetItem(id);
        }
        public T Create(T item)
        {
            return _repository.Create(item);
        }
        public void Update(T item)
        {
            _repository.Update(item);
        }
        public void Remove(ObjectId id)
        {
            _repository.Remove(id);
        }
    }
}
