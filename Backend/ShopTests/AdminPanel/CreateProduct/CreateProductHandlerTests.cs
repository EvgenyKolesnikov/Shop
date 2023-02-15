using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;
using Shop.Repository;
using Xunit;

namespace Shop.AdminPanel.Handlers.Tests
{
    public class CreateProductHandlerTests
    {
        private readonly ShopDbContext _shopDbContext;
        IProductRepository<Product> _productRepository;
        IMediator _mediator;

        public CreateProductHandlerTests(ShopDbContext shopDbContext, IProductRepository<Product> productRepository, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _productRepository = productRepository;
            _mediator = mediator;
        }


        [Fact]
        public async void AddProduct()
        {
            var command = new CreateProductCommand() { CategoryId = 1,Name = "Чайник",Price = 150,Info = "ddd",Rating = 4 };
            var product = await _mediator.Send(command);

            var response = _shopDbContext.Products.Find(product.Id);

            Assert.Equal(product, response);
        }
    }
}