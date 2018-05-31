using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoApi.Controllers;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject
{
    public class BookModelServiceTest
    {
        private Mock<IRepository<BookModel>> Repository { get; }

        private IBookModelService Service { get; }

        public  BookModelServiceTest()
        {
            var books = new List<BookModel>
            {
                new BookModel
                {
                    Id = new MongoDB.Bson.ObjectId(),
                    Title = "C# Tutaj",
                    BookId =3
                }
            };

            Repository = new Mock<IRepository<BookModel>>();

            Repository.Setup(x => x.GetItems()).Returns(books);
            Repository.Setup(x => x.GetItem(It.IsAny<ObjectId>())).Returns((ObjectId id) => books.Find(s=>s.Id == id));
            Repository.Setup(x => x.Create(It.IsAny<BookModel>())).Callback((BookModel model) => books.Add(model));
            Repository.Setup(x => x.Update(It.IsAny<BookModel>())).Callback((BookModel model) => books[books.FindIndex(x=>x.Id == model.Id)] = model);
            Repository.Setup(x => x.Remove(It.IsAny<ObjectId>())).Callback((ObjectId id) => books.RemoveAt(books.FindIndex(x => x.Id == id)));

        }

        [Fact]
        public void ItShouldGetAllItems()
        {
            var books = Service.GetItems();

            Repository.Verify(s => s.GetItems(), Times.Once);
        
        }
    }
}
