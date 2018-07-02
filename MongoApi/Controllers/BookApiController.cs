using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using DataAcces.DAL.Models;
using MongoApi.Models;
using AutoMapper;

namespace MongoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookApiController : Controller 
    {
        private IBookModelService _bookService;
        private readonly IMapper _mapper;

        public BookApiController(IBookModelService dsObject, IMapper mapper)
        {
            _bookService = dsObject;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BookViewModel> Get()
        {
            var itemList = _bookService.GetItems();

            IEnumerable<BookViewModel> modelList = new List<BookViewModel>();

            modelList = _mapper.Map<IEnumerable<BookModel>, IEnumerable<BookViewModel>>(itemList);

            return modelList;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var book = _bookService.GetItem(new ObjectId(id));
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

            _bookService.Create(bookModel);

            return new OkObjectResult(bookModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]BookViewModel bookViewModel)
        {
            var bookId = new ObjectId(id);
            var book = _bookService.GetItem(bookId);

            if (book == null)
            {
                return NotFound();
            }
            var bookModel = _mapper.Map(bookViewModel, book);
            _bookService.Update(bookModel);

            return new OkResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.GetItem(new ObjectId(id));

            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book._Id);
            return new OkResult();
        }

        [HttpPut("bookRent/{id}")]
        public IActionResult BookRent(string id, [FromBody]ClientViewModel clientModel)
        {
            var client = _mapper.Map<Client>(clientModel);
            var book = _bookService.GetItem(new ObjectId(id));
            var booUpdated = _bookService.BookRent(client, book);

            return new OkObjectResult(booUpdated);
        }
    }
}