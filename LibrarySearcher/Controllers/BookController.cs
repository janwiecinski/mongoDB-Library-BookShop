using Microsoft.AspNetCore.Mvc;
using LibrarySearcher.Services;
using System.Linq;
using LibrarySearcher.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using Amazon.S3;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySearcher.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IAmazonS3 _amazon3S;

        public BookController(IBookService bookService, IAuthorService authorService, IAmazonS3 amazon3S)
        {
            _bookService = bookService;
            _authorService = authorService;
            _amazon3S = amazon3S;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSearchedBooks(string query)
        {
            var books = _bookService.GetBooks();

            var result = books.Select(p => new BookListItemViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = _authorService.GetById(p.AuthorId),
                    User = ""
                }
            )
                .Where(s => s.Title.Contains(query) || (s.Author != null && s.Author.LastName.Contains(query)))
                .ToList();

            return View("BookResult", result);
        }

        [HttpPost]
        public async Task<JsonResult> CreateBookPackage(List<BookListItemViewModel> listdata)
        {
            var package = JsonConvert.SerializeObject(new PackageViewModel
            {
                Books = listdata,
                CreatedBy = "User"
            });

            var amazonSend = new AmazonSendService(_amazon3S);

            var keyName = DateTime.UtcNow.ToString()+ " BookListItemViewModel";

            await amazonSend.WritingAnObjectAsync(package, keyName);

            return new JsonResult(package);
        }
    }
}