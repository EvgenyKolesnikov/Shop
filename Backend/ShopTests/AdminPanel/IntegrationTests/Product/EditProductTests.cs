using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.EditProduct;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests.Product
{
    [Collection("IntegrationTests")]
    public class EditProductTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public EditProductTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }


        /// <summary>
        /// Редактирование Продукта, и установка значений FeatureValues
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditProduct()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = null });

            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Процессор" });

            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Цвет" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Ширина" });

            var product = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category.Category.Id,
                Price = 150000,
                Info = "Ноутбук Apple MacBook Pro A2485, 16.2 Apple M1 Max 10 core 32ГБ,1ТБ SSD,Mac OS,MK1A3B / A,серый космос "
            });

            //Act
            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product.Product.Id,
                Name = "EditedName",
                Price = 1000,
                Info = "EditedInfo",
                CategoryId = category2.Category.Id,
                FeatureValue = new Dictionary<int, string>() {
                { feature3.Feature.Id, "Acer" },
                { feature4.Feature.Id, "AMD Ryzen 5 3500U" }
            }
            });


            //Assert
            var productDb = _shopDbContext.Products.Find(product.Product.Id);

            var features = productDb.FeatureValues.Select(i => i.Value).ToList();

            Assert.Equal("EditedName", productDb.Name);
            Assert.Equal("EditedInfo", productDb.Info);
            Assert.Equal(1000, productDb.Price);
            Assert.Equal(category2.Category.Name, productDb.Category.Name);
            Assert.Contains("Acer", features);
            Assert.Contains("AMD Ryzen 5 3500U", features);
        }


        /// <summary>
        /// Редактирование продукта, без смены категории
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditProductWithOutChangeCategory()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = null });

            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Процессор" });

            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Цвет" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Ширина" });

            var product = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category.Category.Id,
                Price = 150000,
                Info = "Ноутбук Apple MacBook Pro A2485, 16.2 Apple M1 Max 10 core 32ГБ,1ТБ SSD,Mac OS,MK1A3B / A,серый космос "
            });

            //Act
            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product.Product.Id,
                Name = "EditedName",
                Price = 1000,
                Info = "EditedInfo",
                CategoryId = category.Category.Id,
                FeatureValue = new Dictionary<int, string>() {
                { feature1.Feature.Id, "Acer" },
                { feature2.Feature.Id, "AMD Ryzen 5 3500U" }
            }
            });

            //Assert
            var productDb = _shopDbContext.Products.Find(product.Product.Id);

            var features = productDb.FeatureValues.Select(i => i.Value).ToList();


            Assert.Equal("EditedName", productDb.Name);
            Assert.Equal("EditedInfo", productDb.Info);
            Assert.Equal(1000, productDb.Price);
            Assert.Equal(category.Category.Name, productDb.Category.Name);
            Assert.Contains("Acer", features);
            Assert.Contains("AMD Ryzen 5 3500U", features);
        }


        /// <summary>
        /// Value в FeatureValue не должны удаляться, при повторном вызове эндпоинта EditProduct
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditProductWithOutChangeCategory_DoubleCall()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = null });

            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category.Category.Id, Name = "Процессор" });

            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Цвет" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Ширина" });

            var product = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category.Category.Id,
                Price = 150000,
                Info = "Ноутбук Apple MacBook Pro A2485, 16.2 Apple M1 Max 10 core 32ГБ,1ТБ SSD,Mac OS,MK1A3B / A,серый космос "
            });

            //Act
            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product.Product.Id,
                Name = "EditedName",
                Price = 1000,
                Info = "EditedInfo",
                CategoryId = category.Category.Id,
                FeatureValue = new Dictionary<int, string>() {
                { feature1.Feature.Id, "Acer" },
                { feature2.Feature.Id, "AMD Ryzen 5 3500U" }
            }
            });

      
            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product.Product.Id,
                Name = "EditedName",
                Price = 1000,
                Info = "EditedInfo",
                CategoryId = category.Category.Id,
                FeatureValue = new Dictionary<int, string>() {
                { feature1.Feature.Id, "Acer" },
                { feature2.Feature.Id, "AMD Ryzen 5 3500U" }
            }
            });

            //Assert
            var productDb = _shopDbContext.Products.Find(product.Product.Id);

            var features = productDb.FeatureValues.Select(i => i.Value).ToList();

            Assert.Equal("EditedName", productDb.Name);
            Assert.Equal("EditedInfo", productDb.Info);
            Assert.Equal(1000, productDb.Price);
            Assert.Equal(category.Category.Name, productDb.Category.Name);
            Assert.Contains("Acer", features);
            Assert.Contains("AMD Ryzen 5 3500U", features);
        }
    }
}
