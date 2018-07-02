using System.Collections.Generic;

namespace LibrarySearcher.Models
{
    public class BookViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public IEnumerable<BookCopyModel> BookCopy { get; set; }
    }
}
