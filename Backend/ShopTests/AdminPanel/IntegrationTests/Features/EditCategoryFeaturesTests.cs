using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.EditFeature;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests.Features
{
    [Collection("IntegrationTests")]
    public class EditCategoryFeaturesTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;


        public EditCategoryFeaturesTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        [Fact]
        public async void EditTest()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет" } });
           
            var editableFeature = await _mediator.Send(new EditFeatureCommand() {Id = category.Category.Features.First().Id,Name = "Бренд" });

            var feature = _shopDbContext.Features.First();

            Assert.Equal("Бренд", feature.Name);
        }

       
    }
}
