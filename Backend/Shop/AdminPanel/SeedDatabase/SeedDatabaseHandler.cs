using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.EditProduct;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.SeedDatabase
{
    public class SeedDatabaseHandler : IRequestHandler<SeedDatabaseCommand,string>
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IMediator _mediator;
        public SeedDatabaseHandler(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        public async Task<string> Handle(SeedDatabaseCommand command, CancellationToken cancellationToken)
        {
            var Categories = _shopDbContext.Categories;
            var Products = _shopDbContext.Products;
            var Features = _shopDbContext.Features;
          //  var CategoriesChild = _shopDbContext.CategoryChilds;

            _shopDbContext.RemoveRange(Categories);
         //  _shopDbContext.RemoveRange(CategoriesChild);
            _shopDbContext.RemoveRange(Products);
            _shopDbContext.RemoveRange(Features);
            _shopDbContext.SaveChanges();

            var category1 = await _mediator.Send(new CreateCategoryCommand() { Name = "Мебель", ParentCategoryId = null });
            var category2 = await _mediator.Send(new CreateCategoryCommand() { Name = "Электроника", ParentCategoryId = null });
            var category3 = await _mediator.Send(new CreateCategoryCommand() { Name = "Телевизоры", ParentCategoryId = category2.Id });
            var category4 = await _mediator.Send(new CreateCategoryCommand() { Name = "Компьютеры", ParentCategoryId = category2.Id });
            var category5 = await _mediator.Send(new CreateCategoryCommand() { Name = "Ноутбуки", ParentCategoryId = category4.Id });
            var category6 = await _mediator.Send(new CreateCategoryCommand() { Name = "Моноблоки", ParentCategoryId = category4.Id });


            var feature1 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category2.Id, Name = "Бренд" });
            var feature2 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Id, Name = "CPU" });
            var feature3 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Id, Name = "GPU" });
            var feature4 = await _mediator.Send(new CreateCategoryFeaturesCommand() { CategoryId = category4.Id, Name = "RAM" });

            var product1 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "MacBook Pro",
                CategoryId = category5.Id,
                Price = 150000,
                Rating = 8.4f,
                Info = "Ноутбук Apple MacBook Pro A2485, 16.2 Apple M1 Max 10 core 32ГБ,1ТБ SSD,Mac OS,MK1A3B / A,серый космос "
            });

            var product2 = await _mediator.Send(new CreateProductCommand()
            {
                Name = "Acer Aspire 3",
                CategoryId = category5.Id,
                Price = 43500,
                Rating = 7.6f,
                Info = "Ноутбук Acer Aspire 3 A315-23-R5B8, 15.6 AMD Ryzen 5 3500U 2.1ГГц, 8ГБ, 1ТБ, AMD Radeon Vega 8,  Eshell, NX.HVUER.006, серебристый"
            });

            await _mediator.Send(new EditProductCommand() { ProductId = product1.Id, FeatureValue = new Dictionary<int, string>() { 
                { feature1.Id, "Apple" },
                { feature2.Id, "M1" },
                { feature3.Id, "интегрированный" },
                { feature4.Id, "16 ГБ" }
            } });

            await _mediator.Send(new EditProductCommand()
            {
                ProductId = product2.Id,FeatureValue = new Dictionary<int, string>() {
                { feature1.Id, "Acer" },
                { feature2.Id, "AMD Ryzen 5 3500U" },
                { feature3.Id, "AMD Radeon Vega 8" },
                { feature4.Id, "8 ГБ" }
            }
            });





            return "Success";
        }
    }
}
