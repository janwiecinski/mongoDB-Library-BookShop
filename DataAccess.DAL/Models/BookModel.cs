using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DataAcces.DAL.Models
{
    public class BookModel : BaseModel
    {
        [BsonElement("Title")]
        [BsonIgnoreIfNull]
        public string Title { get; set; }

        [BsonElement("Author_Id")]
        [BsonIgnoreIfNull]
        public  ObjectId Author_Id { get; set; }

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

    public class Author : BaseModel
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

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
        public DateTime AssumedReturnDate { get; set; }

        [BsonElement("FactReturnDate")]
        [BsonIgnoreIfNull]
        public DateTime FactReturnDate { get; set; }

        [BsonElement("PenaltyCost")]
        [BsonIgnoreIfNull]
        public int PenaltyCost { get; set; }

        [BsonElement("Client_Id")]
        public  ObjectId Client_Id { get; set; }
    }

    public class Client : BaseModel
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }
    }
}
