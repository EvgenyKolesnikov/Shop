using MediatR;
using Shop.AdminPanel.ChangeCountProduct;
using Shop.AdminPanel.CreateProduct;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace Shop.AdminPanel.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class ChangeCountProductChangesTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;


        public ChangeCountProductChangesTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        [Fact]
        public async void ChangeCountProduct()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();
            var addCommand = new CreateProductCommand() { CategoryId = 1, Name = "Чайник", Price = 150 };
            var addProductResponse = await _mediator.Send(addCommand);

            //Act
            var changeCountProductcommand = new ChangeCountProductCommand() { ProductId = addProductResponse.Product.Id, Count = 100 };
            await _mediator.Send(changeCountProductcommand);

            //Assert
            var product = await _shopDbContext.Products.FindAsync(addProductResponse.Product.Id);
            Assert.Equal(100, product.Count);
        }

        [Fact]
        public async void ProductIdNotExist()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            //Act
            var changeCountProductcommand = new ChangeCountProductCommand() { ProductId = 1, Count = 100 };
            await _mediator.Send(changeCountProductcommand);


            //Assert
            var productsCount = _shopDbContext.Products.Count();
            Assert.Equal(0, productsCount);
        }
    }
}
