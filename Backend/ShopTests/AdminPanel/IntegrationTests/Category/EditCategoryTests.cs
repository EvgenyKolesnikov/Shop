using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.EditCategory;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopTests.AdminPanel.IntegrationTests.Category
{
    public class EditCategoryTests
    {
        private readonly ShopDbContext _shopDbContext;
        IMediator _mediator;

        public EditCategoryTests(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async void EditCategory()
        {
            _shopDbContext.TruncateAllTables();

            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null, Features = new List<string>() { "Бренд", "Цвет" } });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Компьютеры", ParentCategoryId = category1.Id, Features = new List<string>() { "Стационарные" } });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category2.Id, Features = new List<string>() { "Ширина" } });
            var category4 = await _mediator.Send(new CreateCategoryCommand() { Name = "Моноблоки", ParentCategoryId = category2.Id, Features = new List<string>() { "Ширина" } });

            var editedCategory = await _mediator.Send(new EditCategoryCommand() {CategoryId = category2.Id, Name = "Компьютеры2",ParentCategoryId = category3.Id });

            var categoryDb1 = _shopDbContext.Categories.Find(category1.Id);
            var categoryDb2 = _shopDbContext.Categories.Find(category2.Id);
            var categoryDb3 = _shopDbContext.Categories.Find(category3.Id);
            var categoryDb4 = _shopDbContext.Categories.Find(category4.Id);


            Assert.Null(categoryDb1.ParentCategory);
            Assert.Equal(categoryDb2.ParentCategory, categoryDb3);
            Assert.Equal(categoryDb3.ParentCategory, categoryDb1);
            Assert.Equal(categoryDb4.ParentCategory, categoryDb1);

            Assert.Equal(categoryDb2.Name, "Компьютеры2");
        }
    }
}
