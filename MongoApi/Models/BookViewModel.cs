using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoApi.Models
{
    public class BookViewModel
    {

        [BsonElement("Title")]
        [BsonIgnoreIfNull]
        public string Title { get; set; }

        [BsonElement("Author")]
        [BsonIgnoreIfNull]
        public AuthorViewModel Author { get; set; }

        [BsonElement("BookCopieModelList")]
        public IList<BookCopieModel> BookCopieModel { get; set; }

    }
    public class BookCopieModel {
        private IEnumerable<BookRentViewModel> _bookRents;

        [BsonElement("BookCopieId")]
        public int BookCopieId { get;  set; }

        [BsonElement("IsAvaible")]
        public bool IsAvaible { get; set; }

        [BsonElement("BookRent")]
        [BsonIgnoreIfNull]
        public IEnumerable<BookRentViewModel> BookRent
        {
            get { return _bookRents ?? (_bookRents = new List<BookRentViewModel>()); }
            set { _bookRents = value; }
        }
    }

    public class AuthorViewModel
    {
        [BsonElement("AuthorId")]
        public int AuthorId { get; set; }

        [BsonElement("AuthorFirstName")]
        public string AuthorFirstName { get; set; }

        [BsonElement("AuthorLastName")]
        public string AuthorLastName { get; set; }
    }

    public class BookRentViewModel
    {
        [BsonElement("OrderId")]
        public int OrderId { get; set; }

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
        public ClientViewModel Client { get; set; }
    }

    public class ClientViewModel
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

