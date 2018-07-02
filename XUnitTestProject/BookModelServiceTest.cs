using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoApi.Controllers;
using MongoApi.Models;
using MongoDB.Bson;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    _Id = new MongoDB.Bson.ObjectId(),
                    Title = "C# Tutaj",
                }
            };

            Repository = new Mock<IRepository<BookModel>>();

            Repository.Setup(x => x.GetItems()).Returns(books);
            Repository.Setup(x => x.GetItem(It.IsAny<ObjectId>())).Returns((ObjectId id) => books.Find(s=>s._Id == id));
            Repository.Setup(x => x.Create(It.IsAny<BookModel>())).Callback((BookModel model) => books.Add(model));
            Repository.Setup(x => x.Update(It.IsAny<BookModel>())).Callback((BookModel model) => books[books.FindIndex(x=>x._Id == model._Id)] = model);
            Repository.Setup(x => x.Remove(It.IsAny<ObjectId>())).Callback((ObjectId id) => books.RemoveAt(books.FindIndex(x => x._Id == id)));

        }

        [Fact]
        public void ItShouldGetAllItems()
        {
            var books = Service.GetItems();

            Repository.Verify(s => s.GetItems(), Times.Once);
        
        }

        [Fact]
        public void ItShouldReturnJsonGoogleBook()
        {
            var myStr = "{\n \"items\": [\n  {\n   \"volumeInfo\": {\n   " +
                " \"title\": \"$2.00 a Day\"\n   }\n  },\n  {\n   \"volumeInfo\": " +
                "{\n    \"title\": \"New World A-Coming\"\n   }\n  },\n  {\n   \"volumeInfo\":" +
                " {\n    \"title\": \"A Handbook of Research Methods for Clinical and Health Psychology\"\n " +
                "  }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"Venice\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n" +
                "    \"title\": \"A Geography of Victorian Gothic Fiction\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"Late Antiquity\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"Peace As a Women's Issue\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"A Holocaust Reader\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"Shostakovich\"\n   }\n  },\n  {\n   \"volumeInfo\": {\n    \"title\": \"Reports from a Wild Country\"\n   }\n  }\n ]\n}\n";

            var result = JsonConvert.DeserializeObject<GoogleBooksResponse>(myStr);

            var strTitles = result.items.Select(s => s.volumeInfo.title).ToList();
        }
    }
}
