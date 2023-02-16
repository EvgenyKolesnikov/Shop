using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.Database;
using Shop.Model;
using Shop.Repository;
using Xunit;
using Xunit.Priority;
using Xunit.Sdk;

namespace Shop.AdminPanel.IntegrationTests
{
    public class CreateProductTests 
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public CreateProductTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        
        [Fact]
        public async void AddProduct()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();
            var command = new CreateProductCommand() { CategoryId = 1,Name = "Чайник",Price = 150 };

            //Act
            var createProductResponse = await _mediator.Send(command);
            
            //Assert
            var product = await _shopDbContext.Products.FindAsync(createProductResponse.Product.Id);
            Assert.Equal(product, createProductResponse.Product);
        }

        [Fact]
        public async void AddDuplicate()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();
            var command = new CreateProductCommand() { CategoryId = 1, Name = "Чайник" };

            //Act
            await _mediator.Send(command);
            await _mediator.Send(command);

            //Assert
            var response = _shopDbContext.Products.Where(i => i.Name == command.Name).Count();
            Task.WaitAll();
            Assert.Equal(1,response);
        }
    }
}