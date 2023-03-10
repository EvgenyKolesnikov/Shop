using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.DeleteFeature;
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
    public class DeleteCategoryFeaturesTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;


        public DeleteCategoryFeaturesTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        [Fact]
        public async void DeleteCategoryFeatures()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет" } });

            var feature = await _mediator.Send(new DeleteFeatureCommand() {Id = category.Features.First().Id });

            var features = await _shopDbContext.Features.ToListAsync(); 
            
            Assert.Empty(features);
        }

        [Fact]
        public async void DeleteCategoryFeatures_DeleteEmptyItem()
        {
            _shopDbContext.TruncateAllTables();

            var feature = await _mediator.Send(new DeleteFeatureCommand() { Id = 1 });

            var features = await _shopDbContext.Features.ToListAsync();

            Assert.Equal("Feature Not Found", feature.result);
        }
    }
}
