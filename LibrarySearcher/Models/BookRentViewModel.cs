using System;

namespace LibrarySearcher.Models
{
    public class BookRentViewModel
    {
        public string Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime AssumedReturnDate { get; set; }
        public DateTime RealReturnDate { get; set; }
        public int PenaltyCost { get; set; }
        public string ClientId { get; set; }
    }
}
