using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }  // Change this to ObjectId instead of string

        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}
