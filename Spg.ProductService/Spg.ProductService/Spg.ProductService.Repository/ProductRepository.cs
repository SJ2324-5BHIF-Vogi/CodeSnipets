using MongoDB.Driver;
using Spg.ProductService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ProductService.Repository
{
    public class ProductRepository
    {
        private readonly FilterDefinitionBuilder<Product> _filterDefinition = Builders<Product>.Filter;
        // Dependency Injection

        public List<Product> Get()
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("Catalog");
            IMongoCollection<Product> products = database.GetCollection<Product>("products");

            return products.Find(_filterDefinition.Empty).ToList();
        }
    }
}
