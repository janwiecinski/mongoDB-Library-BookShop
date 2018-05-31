using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoApi.Models
{
    public class BookViewModel
    {
        [BsonElement("BookId")]
        public int BookId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Author")]
        public  AuthorViewModel Author { get; set; }

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
        [BsonElement("AuthId")]
        public int AuthId { get; set; }

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

        [BsonElement("Client")]
        public  ClientViewModel Client { get; set; }
    }

    public class ClientViewModel
    {
        [BsonElement("ClientId")]
        public string ClientId { get;}

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
    }
}

