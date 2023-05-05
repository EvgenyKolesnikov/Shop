using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.EditProduct;
using Shop.Database;
using Shop.Model;
using ShopApp.Common;
using ShopApp.Repository.Products;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class GetProductsTests
    {
        IProductRepository<Product> _productRepository;
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public GetProductsTests(IProductRepository<Product> productRepository,ShopDbContext shopDbContext, IMediator mediator)
        {
            _productRepository = productRepository;
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }



        [Fact]
        public async Task GetProductsFilterTest()
        {
            //Arrange
            _shopDbContext.TruncateAllTables();

            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category1.Category.Id });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Мебель", ParentCategoryId = null });

            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category1.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category1.Category.Id, Name = "Цвет" });
            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Процессор" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category3.Category.Id, Name = "Цвет" });


            var product1 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category2.Category.Id,
                Price = 100000,
                Count = 0,
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue{Id = feature1.Feature.Id, Value = "Apple" },
                    new FeatureIdValue{Id = feature2.Feature.Id, Value = "Белый" },
                    new FeatureIdValue{Id = feature3.Feature.Id, Value = "M1" }
                }
            });

            var product2 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "Lenovo book",
                CategoryId = category2.Category.Id,
                Price = 150000,
                Count = 100,
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue{Id = feature1.Feature.Id, Value = "Lenovo" },
                    new FeatureIdValue{Id = feature2.Feature.Id, Value = "Серый" },
                    new FeatureIdValue{Id = feature3.Feature.Id, Value = "Intel" }
                }
            });

            var product3 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "Холодильник",
                CategoryId = category1.Category.Id,
                Price = 55000,
                Count = 9,
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue{Id = feature1.Feature.Id, Value = "Samsung" },
                    new FeatureIdValue{Id = feature2.Feature.Id, Value = "Желтый" }
                }
            });


            var product4 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "Шкаф",
                CategoryId = category3.Category.Id,
                Price = 150000,
                Count = 200,
              
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue{Id = feature1.Feature.Id, Value = "IKEA" },
                    new FeatureIdValue{Id = feature4.Feature.Id, Value = "Черный" }
                }
            });


            // Test Category Ids 
            // Дочерние категории должны выходить
            var filter = new ProductFilterPrompt()
            {
                CategoryIds = new List<int?> { category1.Category.Id, category3.Category.Id }
            };
            var products = _productRepository.GetList(filter).ToList();

            Assert.Equal(4 , products.Count());


            // Test Category Ids #2
            // Родительские категории не должны входить
            filter = new ProductFilterPrompt()
            {
                CategoryIds = new List<int?> { category2.Category.Id, null }
            };
            products = _productRepository.GetList(filter).ToList();
            Assert.Equal(2, products.Count());

            // Test Nullable 
            filter = new ProductFilterPrompt()
            {
                
            };
            products = _productRepository.GetList(filter).ToList();
            Assert.Equal(4, products.Count());

         
            // Test Price
            filter = new ProductFilterPrompt()
            {
                Price = new Prompt() { From = 60000 , To = 100000 }
            };
            products = _productRepository.GetList(filter).ToList();
            Assert.Equal(1, products.Count());

     

            // Test Complex #2
            filter = new ProductFilterPrompt()
            {
                CategoryIds = new List<int?> { category1.Category.Id },
                Price = new Prompt() { From = 150000, To = 150000},
                Count = new Prompt() { From = 10}
            };
            products = _productRepository.GetList(filter).ToList();
            Assert.Equal(1, products.Count());
            Assert.Contains("Lenovo book", products.Select(i => i.Name).ToList());
        }
    }
}
