using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.GetCategoryFeatures;
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
    public class GetCategoryFeaturesTests
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IMediator _mediator;


        public GetCategoryFeaturesTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }



        [Fact]
        public async void GetCategoryFeatures()
        {
            _shopDbContext.TruncateAllTables();

            var category = await _mediator.Send(new CreateCategoryCommand() { Name = "Продукты", Features = new List<string>() { "Цвет","Ширина" } });
            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", Features = new List<string>() { "Бренд" } });

            var features = await _mediator.Send(new GetCategoryFeaturesQuery() { Id = category.Category.Id });
            var featuresDb = await _shopDbContext.Features.ToListAsync();
            

            Assert.Equal(3, featuresDb?.Count);
            Assert.Equal(2, features.Count());
        }
    }
}
