using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.DeleteProduct;
using Shop.AdminPanel.EditProduct;
using Shop.Database;
using Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.AdminPanel.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class DeleteProductTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public DeleteProductTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }


        /// <summary>
        /// Простое удаление 
        /// </summary>
        [Fact]
        public async void DeleteProduct()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();
            var createProductResponse = await _mediator.Send(new CreateProductCommand() { CategoryId = 1, Name = "Чайник", Price = 150 });

            //Act
            await _mediator.Send(new DeleteProductCommand() { Id = createProductResponse.Product.Id });

            //Assert
            var response = _shopDbContext.Products.ToList().Count();

            Assert.Equal(0, response);
        }


        /// <summary>
        /// При каскадном удалении должен удаляться продукт и все его связи из таблицы FeaturesValues
        /// Таблицы Features и Categories должны быть нетронутыми
        /// </summary>
        [Fact]
        public async void ProductCascadeDelete()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = null });

            var feature = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Процессор" });
            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Видеокарта" });

            var product = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category.Category.Id,
            });

            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product.Product.Id,
                FeatureValue = new Dictionary<int, string>() {
                { feature.Feature.Id, "Acer" },
                { feature2.Feature.Id, "AMD Ryzen 5 3500U" },
                { feature3.Feature.Id, "AMD Radeon Vega 8" },
            }
            });

            //Act
            await _mediator.Send(new DeleteProductCommand() { Id = product.Product.Id });


            //Assert
            var productsCount = _shopDbContext.Products.ToList().Count();
            var featureValuesCount = _shopDbContext.Products.ToList().Count();

            Assert.Equal(0,productsCount);
            Assert.Equal(0,featureValuesCount);

            var categoryResponse = _shopDbContext.Categories.Find(category.Category.Id);
            Assert.Equal(categoryResponse,category.Category);

            var featuresCount = _shopDbContext.Features.ToList().Count();
            Assert.Equal(3,featuresCount);
        }
    }
}
