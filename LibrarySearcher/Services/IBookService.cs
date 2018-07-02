using LibrarySearcher.Models;
using System.Collections.Generic;

namespace LibrarySearcher.Services
{
    public interface IBookService
    {
        IEnumerable<BookViewModel> GetBooks();
    }
}
