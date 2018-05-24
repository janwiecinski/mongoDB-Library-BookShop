using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DataAcces.DAL.Models
{
    public class BookModel : BaseModel
    {
        [BsonElement("BookId")]
        public int BookId { get; set; }

        [BsonElement("Title")]
        [BsonIgnoreIfNull]
        public string Title { get; set; }

        [BsonElement("Author")]
        [BsonIgnoreIfNull]
        public  Author Author { get; set; }

        [BsonElement("BookCopyItems")]
        public  IEnumerable<BookCopy> BookCopyItems{ get; set; }
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
        public  IList<BookRent> BookRent
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

    public class BookRent { 

        private string strId;

        [BsonElement("OrderId")]
        public string OrderId
        {
            get { if (strId == null)
                { return strId = Guid.NewGuid().ToString(); }
                  return strId ; }
            set { strId = value; }
        }

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
        public  Client Client { get; set; }
    }

    public class Client
    {
        private string strId;
        [BsonElement("ClientId")]
        public string ClientId {
            get
            {
                if (strId == null)
                { return strId = Guid.NewGuid().ToString(); }
                  return strId;
            }
            set { strId = value; }
        }

        [BsonElement("ClientFirstName")]
        public string ClientFirstName { get; set; }

        [BsonElement("ClientLastName")]
        public string ClientLastName { get; set; }

        [BsonElement("ClientEmail")]
        public string ClientEmail { get; set; }
    }
}
