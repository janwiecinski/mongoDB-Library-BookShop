using DataAcces.DAL.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MongoApi.Controllers
{
    public interface IBookModelService
    {
        IEnumerable<BookModel> GetItems();
        BookModel GetItem(ObjectId id);
        BookModel Create(BookModel item);
        void Update(BookModel item);
        void Remove(ObjectId id);
        BookModel BookRent(Client client, BookModel book);
    }
}
