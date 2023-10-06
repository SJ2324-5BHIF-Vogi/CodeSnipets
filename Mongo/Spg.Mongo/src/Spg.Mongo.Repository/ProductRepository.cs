using MongoDB.Driver;
using Spg.Mongo.DomainModel.Model;

namespace Spg.Mongo.Repository;
public class ProductRepository
{
    //private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Product> _productsCollection;
    private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;

    public ProductRepository(IMongoDatabase database, string collectionName)
    {
        //_database = database;
        //MongoClient client = new MongoClient("mongodb://localhost:27017");
        //IMongoDatabase database = client.GetDatabase("Catalog");
        _productsCollection = database.GetCollection<Product>("collectionName");
    }

    public IEnumerable<Product> GetAll()
    {
        //FilterDefinition<Product> filter = filterBuilder.Eq(x => x.Id, 1);
        return _productsCollection.Find(filterBuilder.Empty).ToList();
    }

    public Product Create()
    {
        //Product product = new Product() { Id = 2, Name = "Product 2", Description = "Product 2" };
        Product product = new Product() { Id = 4, Name = "Product 3", Description = "Product 3", CreationDate = DateTime.Now };
        _productsCollection.InsertOneAsync(product);
        return product;
    }
}
