using System;

namespace LibrarySearcher.Models
{
    public class BookListItemViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public AuthorViewModel Author { get; set; }
        public string User { get; set; }
    }
}
