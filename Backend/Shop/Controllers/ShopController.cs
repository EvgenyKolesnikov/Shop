using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel.GetCategoryFeatures;
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
        private readonly IMediator _mediator;

        public ShopController(IProductRepository<Product> productRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _mediator = mediator;
        }


        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProducts")]
        public IEnumerable<ProductResponseDTO> GetProducts()
        {
            var products = _productRepository.GetList().ToList();

            var response = new List<ProductResponseDTO>();

            products.ForEach(product => response.Add(new ProductResponseDTO(product)));

            return response;
        }

        /// <summary>
        /// Получить конкретный продукт
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetProductById")]
        public ProductResponseDTO GetProductById(int Id)
        {
            var product = _productRepository.GetById(Id);

            var response = new ProductResponseDTO(product);

            return response;
        }

        /// <summary>
        /// Получить список всех атрибутов
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFeatures")]
        public IEnumerable<Feature> GetFeatures()
        {
            var response = _productRepository.GetFeatures();

            return response;
        }

        /// <summary>
        /// Получить список категорий в виде дерева
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCategoriesTree")]
        public IEnumerable<CategoryResponseTreeDTO> GetCategoriesTree()
        {
            var categories = _productRepository.GetCategoriesTree().ToList();

            var response = new List<CategoryResponseTreeDTO>();

            categories.ForEach(category => response.Add(new CategoryResponseTreeDTO(category)));

            return response;
        }


        /// <summary>
        /// Получить список категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCategories")]
        public IEnumerable<CategoryResponseDTO> GetCategories()
        {
            var categories = _productRepository.GetCategories().ToList();

            var response = new List<CategoryResponseDTO>();

            categories.ForEach(category => response.Add(new CategoryResponseDTO(category)));

            return response;
        }




        /// <summary>
        /// Получить список атрибутов конкретной категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetFeaturesByCategory/{id}")]
        public async Task<IEnumerable<Feature>> GetFeaturesByCategory([FromRoute] int id)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            cancelTokenSource.CancelAfter(4000);


            var command = new GetCategoryFeaturesQuery() { Id = id };
            var response = await _mediator.Send(command, token);

            return response;
        }


        /// <summary>
        /// Получить список продуктов конкретной категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetProductsByCategory/{id}")]
        public async Task<IEnumerable<ProductResponseDTO>> GetProductsByCategoryd([FromRoute] int id)
        {

            var response = _productRepository.GetProductsByCategory(id);
            return response;
        }
    }
}
