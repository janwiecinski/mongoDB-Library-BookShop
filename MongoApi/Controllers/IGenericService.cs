using Microsoft.AspNetCore.Mvc;
using MongoApi.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    public interface IGenericService<T>
    {
        IEnumerable<T> GetItems();
        T GetItem(ObjectId id);
        T Create(T item);
        void Update(T item);
        void Remove(ObjectId id);
    }
}
