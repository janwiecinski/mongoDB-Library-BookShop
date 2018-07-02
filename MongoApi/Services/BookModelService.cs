using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using Hangfire;
using Microsoft.Extensions.Options;
using MongoApi.Utilts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoApi.Controllers
{
    public class BookModelService: IBookModelService
    {
        private readonly IRepository<BookModel> _repository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Client> _clientRepository;

        private readonly SendMessageParams _options;


        public BookModelService(IRepository<Client> clientRepository, IRepository<Author> authorRepository, IRepository<BookModel> repository, IOptions<SendMessageParams> options)
        {
            _clientRepository = clientRepository;
            _authorRepository = authorRepository;
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

            client._Id = ObjectId.GenerateNewId();
            Task.Run(()=> _clientRepository.Create(client));

            try
            {
                var copy = book.BookCopyItems.FirstOrDefault(c => c.IsAvailable);
                copy.IsAvailable = false;
                var bookRent = new BookRent
                {
                    RentDate = System.DateTime.Now,
                    AssumedReturnDate = System.DateTime.Now.AddDays(30),
                    Client_Id = client._Id
               
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

       
    }
}
