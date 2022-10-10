using Microsoft.AspNetCore.Mvc;
using Shop.Database;
using Shop.Model;
using Shop.Repository;
using Shop.Repository.Response;
using System.Diagnostics;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        IProductRepository<Product> _productRepository;

        public ShopController(IProductRepository<Product> productRepository, ShopDbContext shopDbContext)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetProducts")]
        public IEnumerable<ProductResponseDTO> GetProducts()
        {
            var products = _productRepository.GetList().ToList();

            var response = new List<ProductResponseDTO>();

            products.ForEach(product => response.Add(new ProductResponseDTO(product)));

            return response;
        }

        [HttpGet("GetProductById")]
        public ProductResponseDTO GetProductById(int Id)
        {
            var product = _productRepository.GetById(Id);

            var response = new ProductResponseDTO(product);

            return response;
        }

        [HttpGet("GetFeatures")]
        public IEnumerable<Feature> GetFeatures()
        {
            var response = _productRepository.GetFeatures();

            return response;
        }

        [HttpGet("GetCategories")]
        public IEnumerable<CategoryResponseDTO> GetCategories()
        {
            var categories = _productRepository.GetCategories().ToList();

            var response = new List<CategoryResponseDTO>();

            categories.ForEach(category => response.Add(new CategoryResponseDTO(category)));

            return response;
        }

      
    }
}
