using System.Collections.Generic;

namespace LibrarySearcher.Models
{
    public class BookCopyModel
    {
        public int Id { get; set; }
        public bool IsAvaiable { get; set; }
        public List<BookRentViewModel> BookRentList { get; set; }
    }
}
