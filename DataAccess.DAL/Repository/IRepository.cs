using DataAcces.DAL.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAcces.DAL.Repository
{
    public interface IRepository<T> where T : BaseModel
    {

        IEnumerable<T> GetItems();

        T GetItem(ObjectId id);

        T Create(T entity);

        void Update(T entity);

        void Remove(ObjectId id);
    }
}
