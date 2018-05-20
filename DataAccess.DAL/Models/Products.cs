using MongoDB.Bson.Serialization.Attributes;

namespace DataAcces.DAL.Models
{
    public class Products: BaseModel
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
