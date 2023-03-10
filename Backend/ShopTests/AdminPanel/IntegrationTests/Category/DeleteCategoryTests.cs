using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.DeleteCategory;
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
    public class DeleteCategoryTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public DeleteCategoryTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        /// <summary>
        /// При удалении категории, дочерним категориям присваивается родительская категория удаленной
        /// </summary>
        [Fact]
        public async void DeleteCategory_Replace()
        {
            _shopDbContext.TruncateAllTables();
            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null, Features = new List<string>() { "Бренд", "Цвет" } });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Компьютеры", ParentCategoryId = category1.Category.Id, Features = new List<string>() { "Стационарные" } });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category2.Category.Id, Features = new List<string>() { "Ширина" } });
            var category4 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category2.Category.Id, Features = new List<string>() { "Ширина" } });

            await _mediator.Send(new DeleteCategoryCommand() { Id = category2.Category.Id });
            
            var categoryDb3 = _shopDbContext.Categories.Find(category3.Category.Id);
            var categoryDb4 = _shopDbContext.Categories.Find(category4.Category.Id);

            Assert.Equal(category1.Category.Id, categoryDb3.ParentCategory.Id);
            Assert.Equal(category1.Category.Id, categoryDb4.ParentCategory.Id);

            await _mediator.Send(new DeleteCategoryCommand() { Id = category1.Category.Id });

            Assert.Null(categoryDb3.ParentCategory);
            Assert.Null(categoryDb4.ParentCategory);
        }
    }
}
