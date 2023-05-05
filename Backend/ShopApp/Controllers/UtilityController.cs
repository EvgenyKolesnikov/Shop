using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.EditProduct;
using Shop.Database;
using Shop.Model;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilityController : ControllerBase
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IMediator _mediator;
        public UtilityController(ShopDbContext shopDbContext,IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        /// <summary>
        /// Очистить БД
        /// </summary>
        [HttpDelete("TruncateDatabase")]
        public void TruncateDatabase()
        {
            _shopDbContext.TruncateAllTables();
        }

        /// <summary>
        /// Заполнить БД Тестовыми данными
        /// </summary>
        /// <returns></returns>
        [HttpPost("SeedDatabase")]
        public async Task<string> SeedDatabase()
        {
            _shopDbContext.TruncateAllTables();    

            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Мебель", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Телевизоры", ParentCategoryId = category2.Category.Id });
            var category4 = await _mediator.Send(new CreateCategoryCommand() { Name = "Компьютеры", ParentCategoryId = category2.Category.Id });
            var category5 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category4.Category.Id });
            var category6 = await _mediator.Send(new CreateCategoryCommand() { Name = "Моноблоки", ParentCategoryId = category4.Category.Id });


            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Category.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Category.Id, Name = "CPU" });
            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Category.Id, Name = "GPU" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Category.Id, Name = "RAM" });

            var product1 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category5.Category.Id,
                Price = 150000,
                Info = "Ноутбук Apple MacBook Pro A2485, 16.2 Apple M1 Max 10 core 32ГБ,1ТБ SSD,Mac OS,MK1A3B / A,серый космос "
            });

            var product2 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "Acer Aspire 3",
                CategoryId = category5.Category.Id,
                Price = 43500,
                Info = "Ноутбук Acer Aspire 3 A315-23-R5B8, 15.6 AMD Ryzen 5 3500U 2.1ГГц, 8ГБ, 1ТБ, AMD Radeon Vega 8,  Eshell, NX.HVUER.006, серебристый"
            });

            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product1.Product.Id,
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue() { Id = feature1.Feature.Id, Value = "Apple" },
                    new FeatureIdValue() { Id = feature2.Feature.Id, Value = "M1" },
                    new FeatureIdValue() { Id = feature3.Feature.Id, Value = "интегрированный" },
                    new FeatureIdValue() { Id = feature4.Feature.Id, Value = "16 ГБ" }
                }
            });

            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product2.Product.Id,
                FeatureValue = new List<FeatureIdValue>()
                {
                    new FeatureIdValue() { Id = feature1.Feature.Id, Value = "Acer"},
                    new FeatureIdValue() { Id = feature2.Feature.Id, Value = "AMD Ryzen 5 3500U"},
                    new FeatureIdValue() { Id = feature3.Feature.Id, Value = "AMD Radeon Vega 8"},
                    new FeatureIdValue() { Id = feature4.Feature.Id, Value = "8 ГБ"}
                }
            });

            return "Success";
        }
    }
}
