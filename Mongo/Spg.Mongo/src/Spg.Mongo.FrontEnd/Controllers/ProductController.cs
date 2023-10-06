using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.Mongo.DomainModel.Dtos;
using Spg.Mongo.DomainModel.Model;
using Spg.Mongo.Repository;

namespace Spg.Mongo.FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;
        private readonly IPublishEndpoint _publishEndPoint;

        public ProductController(ProductRepository repository, IPublishEndpoint publishEndPoint)
        {
            _repository = repository;
            _publishEndPoint = publishEndPoint;
        }

        [HttpGet("")]
        public IActionResult AllProducts()
        {
            //ProductRepository productRepository = new ProductRepository();
            return Ok(_repository.GetAll());
        }

        [HttpPost("")]
        public IActionResult Create()
        {
            //ProductRepository productRepository = new ProductRepository();
            Product product = _repository.Create();

            // Rabbit-MQ
            _publishEndPoint.Publish(new ProductCreated(product.Id, product.Name, product.Description, product.CreationDate));

            return Created("", product);
        }
    }
}
