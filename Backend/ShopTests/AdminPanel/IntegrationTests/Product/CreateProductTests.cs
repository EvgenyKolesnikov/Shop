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
    [Collection("IntegrationTests")]
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

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Цвет" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Ширина" });


            var command = new CreateProductCommand()
            {
                CategoryId = category.Category.Id,
                Name = "Чайник",
                Price = 150,
                FeatureValue = new Dictionary<int, string>() 
                { 
                    { feature1.Feature.Id, "Желтый" },
                    { 123456 , "kek" }  // Wrong Parameter, it must not be write
                }
            };

            //Act
            var createProductResponse = await _mediator.Send(command);
            
            //Assert
            var product = await _shopDbContext.Products.FindAsync(createProductResponse.Product.Id);
            var features = product.FeatureValues.Select(i => i.Value).ToList();

            Assert.Equal(product, createProductResponse.Product);
            
            Assert.Equal(2, features.Count);
            Assert.Equal( "Желтый", product.FeatureValues.First(i => i.Feature.Name == "Цвет").Value);
            Assert.Null( product.FeatureValues.First(i => i.Feature.Name == "Ширина").Value);
        }

        /// <summary>
        /// Добавление дубликатов 
        /// </summary>
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