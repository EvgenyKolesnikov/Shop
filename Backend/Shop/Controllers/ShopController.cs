using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Model;
using Shop.Repository;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        IProductRepository<Product> _productRepository;
        ShopDbContext _shopDbContext;

        public ShopController(IProductRepository<Product> productRepository,ShopDbContext shopDbContext)
        {
            _productRepository = productRepository;
            _shopDbContext = shopDbContext;
        }

        [HttpPost("CreateProduct")]
        public void CreateProduct(Product product)
        {
            _productRepository.Create(product);
        }

        [HttpGet("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetList();
        }

        [HttpGet("GetProductById")]
        public Product GetProductById(int Id)
        {
            return _productRepository.GetById(Id);
        }

        [HttpPut("UpdateProduct")]
        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

        [HttpDelete("DeleteProduct")]
        public void DeleteProduct(Product product)
        {
            _productRepository.Delete(product);
        }
    }
}
