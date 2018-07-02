using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoApi.Models
{
    public class BookViewModel
    {
        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("BookCopyItems")]
        public  IEnumerable<BookCopyModel> BookCopyItems { get; set; }

    }
    public class BookCopyModel
    {
        private IEnumerable<BookRentViewModel> _bookRents;

        [BsonElement("BookCopyId")]
        public int BookCopyId { get;  set; }

        [BsonElement("IsAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("BookRent")]
        [BsonIgnoreIfNull]
        public  IEnumerable<BookRentViewModel> BookRent
        {
            get { return _bookRents ?? (_bookRents = new List<BookRentViewModel>()); }
            set { _bookRents = value; }
        }
    }

    public class AuthorViewModel
    {

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }
    }

    public class BookRentViewModel
    {
        [BsonElement("OrderId")]
        public string OrderId { get;}

        [BsonElement("RentDate")]
        public DateTime RentDate { get; set; }

        [BsonElement("AssumendReturnDate")]
        public DateTime AssumendReturnDate { get; set; }

        [BsonElement("FactReturnDate")]
        [BsonIgnoreIfNull]
        public DateTime FactReturnDate { get; set; }

        [BsonElement("PenaltyCost")]
        [BsonIgnoreIfNull]
        public int PenaltyCost { get; set; }


    }
    public class ClientViewModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}

