using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoApi.Models
{
    public class ProductViewModel
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }

        [BsonElement("ProductName")]
        public string ProductName { get; set; }

        [BsonElement("Price")]
        public int Price { get; set; }

        [BsonElement("Category")]
        public string Catergory { get; set; }
    }
}
