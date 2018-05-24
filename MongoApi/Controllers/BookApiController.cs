using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoApi.Models;
using AutoMapper;
using Hangfire;
using MongoApi.Utilts;
using System;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookApiController : Controller
    {
        private IGenericService<BookModel> _dsObject;
        private readonly IMapper _mapper;

        public BookApiController(IGenericService<BookModel> dsObject, IMapper mapper)
        {
            _dsObject = dsObject;
            _mapper = mapper;

        }

        [HttpGet]
        public IEnumerable<BookViewModel> Get()
        {
            var itemList = _dsObject.GetItems();

            IList<BookViewModel> modelList = new List<BookViewModel>();

            foreach(var item in itemList)
            {
                var bookViewModel = new BookViewModel();
                bookViewModel = _mapper.Map<BookViewModel>(item);
                modelList.Add(bookViewModel);
            }

            return modelList;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var book = _dsObject.GetItem(new ObjectId(id));
            if (book == null)
            {
                return NotFound();
            }
            var resultModel = _mapper.Map<BookViewModel>(book);

            return new ObjectResult(resultModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody]BookViewModel bookViewModel)
        {
            var bookModel = _mapper.Map<BookModel>(bookViewModel);
            _dsObject.Create(bookModel);
            return new OkObjectResult(bookModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]BookViewModel bookViewModel)
        {
            var bookId = new ObjectId(id);
            var book = _dsObject.GetItem(bookId);

            if (book == null)
            {
                return NotFound();
            }
            var bookModel = _mapper.Map(bookViewModel, book);
            _dsObject.Update(bookModel);

            return new OkResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = _dsObject.GetItem(new ObjectId(id));

            if (book == null)
            {
                return NotFound();
            }

            _dsObject.Remove(book.Id);

            return new OkResult();
        }
        [HttpPut("bookRent/{id}")]
        public IActionResult BookRent(string id, ClientViewModel client)
        {
            var book = _dsObject.GetItem(new ObjectId(id));
            var copyList = book.BookCopyItems.GetEnumerator();
            copyList.MoveNext();
            var copy = copyList.Current;

            while (copy.IsAvailable == false)
            {

                if (copyList.MoveNext()==false)
                {
                    return new NotFoundObjectResult("No copy avaiable...");
                }
                copy = copyList.Current;
            }

            copy.IsAvailable = false;

            BackgroundJob.Enqueue(() => SendMessage.SendSimpleMessage());
            BackgroundJob.Schedule(() => SendMessage.SendSimpleMessage(), TimeSpan.FromSeconds(28));

            var bookRent = new BookRent
            {
                RentDate = System.DateTime.Now,
                AssumendReturnDate = System.DateTime.Now.AddDays(30),
                Client = new Client
                {
                    ClientFirstName = client.ClientFirstName,
                    ClientLastName = client.ClientLastName,
                    ClientEmail = client.ClientEmail
                }
            };

            copy.BookRent.Add(bookRent);
            _dsObject.Update(book);

            return new OkResult();
        }
    }
}