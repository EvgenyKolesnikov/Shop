using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests.Features
{
    [Collection("IntegrationTests")]
    public  class CreateCategoryFeaturesTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;


        public CreateCategoryFeaturesTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }



        [Fact]
        public async void CreateCategoryFeatures_SimpleAdd()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет" } });

            var feature = await _mediator.Send(new CreateCategoryFeaturesCommand() { Name = "aa", CategoryId = category.Category.Id });
            
            var features = _shopDbContext.Features.ToList();
            
            Assert.Equal(2, features.Count);
        }

        /// <summary>
        /// Если категория отсуствует, фича не должна создаваться
        /// </summary>
        [Fact]
        public async void CreateCategoryFeatures_AddWithoutCategory()
        {
            _shopDbContext.TruncateAllTables();

            var feature = await _mediator.Send(new CreateCategoryFeaturesCommand() { Name = "aa", CategoryId = 1 });


            var features = _shopDbContext.Features.ToList();

            Assert.Equal("Category not found", feature.Message);
            Assert.Empty(features);

        }


        /// <summary>
        /// Новая фича не должна дублироваться в таблице Features, если она уже существует
        /// </summary>
        [Fact]
        public async void CreateCategoryFeatures_ChangeLink()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет" } });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Мебель", Features = new List<string>() { "Ширина" } });


            var feature = await _mediator.Send(new CreateCategoryFeaturesCommand() { Name = "Ширина", CategoryId = category.Category.Id });

            var features = _shopDbContext.Features.ToList();


            var categoryDb1 = _shopDbContext.Categories.Find(category.Category.Id);
            var categoryDb2 = _shopDbContext.Categories.Find(category2.Category.Id);
            var featuresDb = _shopDbContext.Features.ToList();

            Assert.Equal(2, categoryDb1.Features.Count);
            Assert.Equal(1, categoryDb2.Features.Count);
            Assert.Equal(2, featuresDb.Count);
        }


        /// <summary>
        /// Если новая фича не существует, то создается
        /// </summary>
        [Fact]
        public async void CreateCategoryFeatures_AddFeature()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет" } });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Мебель", Features = new List<string>() { "Ширина" } });


            var feature = await _mediator.Send(new CreateCategoryFeaturesCommand() { Name = "Высота", CategoryId = category.Category.Id });

            var features = _shopDbContext.Features.ToList();


            var categoryDb1 = _shopDbContext.Categories.Find(category.Category.Id);
            var categoryDb2 = _shopDbContext.Categories.Find(category2.Category.Id);
            var featuresDb = _shopDbContext.Features.ToList();

            Assert.Equal(2, categoryDb1.Features.Count);
            Assert.Equal(1, categoryDb2.Features.Count);
            Assert.Equal(3, featuresDb.Count);
        }
    }
}
