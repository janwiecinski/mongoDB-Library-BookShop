using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using DataAcces.DAL.Models;
using DataAcces.DAL.Repository;
using MongoApi.Models;
using AutoMapper;
using System;
using System.Runtime.Serialization;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookApiController : Controller
    {
        private IRepository<BookModel> _dsObject;
        private readonly IMapper _mapper;

        public BookApiController(IRepository<BookModel> dsObject, IMapper mapper)
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

            return new ObjectResult(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody]BookModel p)
        {
            _dsObject.Create(p);
            return new OkObjectResult(p);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]BookModel p)
        {
            var prodId = new ObjectId(id);
            var product = _dsObject.GetItem(prodId);

            if (product == null)
            {
                return NotFound();
            }
            _dsObject.Update(p);

            return new OkResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var product = _dsObject.GetItem(new ObjectId(id));

            if (product == null)
            {
                return NotFound();
            }

            _dsObject.Remove(product.Id);

            return new OkResult();
        }
        [HttpPut("bookRent/{id}")]
        public IActionResult BookRent(string id, Client client)
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