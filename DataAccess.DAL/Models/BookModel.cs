using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DataAcces.DAL.Models
{
    public class BookModel : BaseModel
    {
        private IEnumerable<BookCopy> _bookCopies;

        [BsonElement("Title")]
        [BsonIgnoreIfNull]
        public string Title { get; set; }

        [BsonElement("Author")]
        [BsonIgnoreIfNull]
        public Author Author { get; set; }

        public IEnumerable<BookCopy> BookCopyItems { get; set; }
    }
    public class BookCopy
    {
        private IList<BookRent> _bookRents;

        [BsonElement("BookCopyId")]
        public int BookCopyId { get; set; }

        [BsonElement("IsAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("BookRent")]
        [BsonIgnoreIfNull]
        //CopyRent
        public IList<BookRent> BookRent
        {
            get { return _bookRents ?? (_bookRents = new List<BookRent>()); }
            set { _bookRents = value; }
        }
    }

    public class Author
    {
        [BsonElement("AuthorId")]
        public int AuthorId { get; set; }

        [BsonElement("AuthorFirstName")]
        public string AuthorFirstName { get; set; }

        [BsonElement("AuthorLastName")]
        public string AuthorLastName { get; set; }

    }

    public class BookRent
    {
        [BsonElement("OrderId")]
        public int OrderId { get; set; }

        [BsonElement("RentDate")]
        public DateTime RentDate { get; set; }

        [BsonElement("AssumedReturnDate")]
        public DateTime AssumendReturnDate { get; set; }

        [BsonElement("FactReturnDate")]
        [BsonIgnoreIfNull]
        public DateTime FactReturnDate { get; set; }

        [BsonElement("PenaltyCost")]
        [BsonIgnoreIfNull]
        public int PenaltyCost { get; set; }

        [BsonElement("Client")]
        public Client Client { get; set; }
    }

    public class Client
    {
        [BsonElement("ClientId")]
        public int ClientId { get; set; }

        [BsonElement("ClientFirstName")]
        public string ClientFirstName { get; set; }

        [BsonElement("ClientLastName")]
        public string ClientLastName { get; set; }

        [BsonElement("ClientEmail")]
        public string ClientEmail { get; set; }
    }
}
