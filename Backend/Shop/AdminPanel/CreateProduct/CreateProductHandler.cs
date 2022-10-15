using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var category = _shopDbContext.Categories.Find(command.CategoryId);

            var product = new Product
            {
                Name = command.Name,
                Category = category,
                Info = command.Info,
                Price = command.Price,
                Rating = command.Rating,
            };
            if (category != null)
            {
                foreach (var feature in category.Features)
                {
                    var value = new FeatureValue
                    {
                        Feature = feature,
                        Product = product,
                    };
                    product.Features.Add(value);
                }
            }
            
            await _shopDbContext.Products.AddAsync(product);
            await _shopDbContext.SaveChangesAsync();

            return product.Id;
        }
    }
}
