using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests.Category
{
    [Collection("IntegrationTests")]
    public class CreateCategoryTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public CreateCategoryTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        /// <summary>
        /// Проверка корректности добавления подкатегорий и features
        /// </summary>
        [Fact]
        public async void CategoryTreeCheck()
        {
            _shopDbContext.TruncateAllTables();

            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null, Features = new List<string>() { "Бренд", "Цвет" } });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Компьютеры", ParentCategoryId = category1.Id, Features = new List<string>() { "Стационарные" } });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Комплектующие", ParentCategoryId = category2.Id, Features = new List<string>() { "CPU" } });
            var category4 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category2.Id });

            var features = _shopDbContext.Features.ToList();

            var _category1 = _shopDbContext.Categories.Find(category1.Id);
            var _category2 = _shopDbContext.Categories.Find(category2.Id);
            var _category3 = _shopDbContext.Categories.Find(category3.Id);
            var _category4 = _shopDbContext.Categories.Find(category4.Id);

            var category1Children = category1.ChildCategories;
            var category2Children = category2.ChildCategories;

            Assert.Contains(_category2?.Name, category1Children.Select(i => i.Name));
            Assert.Contains(_category3?.Name, category2Children.Select(i => i.Name));
            Assert.Contains(_category4?.Name, category2Children.Select(i => i.Name));

            Assert.Equal(4, features.Count);
            Assert.Equal(_category3?.ParentCategory?.Id, _category2?.Id);
            Assert.Equal(_category2?.ParentCategory?.Id, _category1?.Id);
        }
    }
}
