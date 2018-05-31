using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoApi.Models;
using MongoApi.Utilts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoApi.Controllers
{
    public class BookModelService: IBookModelService
    {
        private readonly IRepository<BookModel> _repository;
        private readonly SendMessageParams _options;

        public BookModelService(IRepository<BookModel> repository, IOptions<SendMessageParams> options)
        {
            _repository = repository;
            _options = options.Value;
        }

        public IEnumerable<BookModel> GetItems()
        {
            return _repository.GetItems();
        }
        public BookModel GetItem(ObjectId id)
        {
           return _repository.GetItem(id);
        }
        public BookModel Create(BookModel item)
        {
            return _repository.Create(item);
        }
        public void Update(BookModel item)
        {
            _repository.Update(item);
        }
        public void Remove(ObjectId id)
        {
            _repository.Remove(id);
        }
        public BookModel BookRent(Client client, BookModel book)
        {
            BackgroundJob.Schedule(() => SendMessage.SendSimpleMessage(_options), TimeSpan.FromSeconds(28));

            try
            {
                var copy = book.BookCopyItems.FirstOrDefault(c => c.IsAvailable);
                copy.IsAvailable = false;
                var bookRent = new BookRent
                {
                    RentDate = System.DateTime.Now,
                    AssumendReturnDate = System.DateTime.Now.AddDays(30),
                    Client = new Client
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Email = client.Email
                    }
                };
                copy.BookRent.Add(bookRent);

                _repository.Update(book);

                return book;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }

            
        }
        public void InsertMany()
        {
            IList<string> nameTable = new List<string>() { "Jasiek", "Piotr", "Kasia", "Marcel", "Krzyś", "Ola", "Paweł", "Tosia", "Jacek" };
            IList<string> lastNameTable = new List<string>() { "Smith", "Pop", "Głow", "Marcelski", "Krzy", "Olanski", "Pawelski", "Tosiak", "Jacek" };
            IList<BookModel> bookList = new List<BookModel>();
            for (int i = 0; i < 100; i++)
            {
                var random = new Random();
                int nameElement = random.Next(nameTable.Count);
                int lastNameElement = random.Next(lastNameTable.Count);

                var book = new BookModel()
                {
                    Title = $"C# ogień part {i}",
                    BookId = i,
                    Author = new Author
                    {
                        AuthId = i,
                        FirstName = nameTable[nameElement],
                        LastName = nameTable[lastNameElement]
                    },
                    BookCopyItems = new List<BookCopy>
                    {
                        new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 1
                        },
                         new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 2
                        },
                          new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 3
                        },
                           new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 4
                        },
                            new BookCopy
                        {
                         IsAvailable = true,
                         BookCopyId = 5
                        }
                    }
                };

                bookList.Add(book);
            }
            _repository.InsertMany(bookList);


        }
    }
}
