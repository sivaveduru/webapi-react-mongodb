using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>(settings.Value.CollectionName);
        }

        public async Task<List<Product>> GetAllProducts() => await _products.Find(p => true).ToListAsync();

        public async Task<Product> GetProductById(string id)
        {
            // Convert string ID to ObjectId
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var product = await _products.Find(p => p.Id == objectId).FirstOrDefaultAsync();

                // Check if product is null
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                return product;
            }
            else
            {
                // Handle invalid ID format
                throw new ArgumentException("Invalid ID format.");
            }
        }


        public async Task CreateProduct(Product product) => await _products.InsertOneAsync(product);

        public async Task UpdateProduct(string id, Product product)
        {
            // Convert string ID to ObjectId
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                await _products.ReplaceOneAsync(p => p.Id == objectId, product);
            }
            else
            {
                // Handle the case where ID is invalid
                // You can either throw an exception or return an error result
                throw new ArgumentException("Invalid ID format.");
            }
        }
        public async Task DeleteProduct(string id)
        {
            // Convert string ID to ObjectId
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                await _products.DeleteOneAsync(p => p.Id == objectId);
            }
            else
            {
                // Handle the case where ID is invalid
                // You can either throw an exception or return an error result
                throw new ArgumentException("Invalid ID format.");
            }
        }
    }
}
