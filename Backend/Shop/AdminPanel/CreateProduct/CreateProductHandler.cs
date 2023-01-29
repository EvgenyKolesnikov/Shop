using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var category = _shopDbContext.Categories.Find(command.CategoryId);

            var categories = GetParents(category);
            var features = GetAllFeatures(categories);


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
                foreach (var feature in features)
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

            return product;
        }

        private List<Category> GetParents(Category category)
        {
            var result = new List<Category>();
            
            result.Add(category);

            var ParentCategory = category.ParentCategory;
            while (ParentCategory != null)
            {
                result.Add(ParentCategory);
                ParentCategory = ParentCategory.ParentCategory;
            }
        

            return result;
        }

        private List<Feature> GetAllFeatures(List<Category> categories)
        {
            var result = new List<Feature>();

            foreach(var category in categories)
            {
                result.AddRange(category.Features);
            }

            return result;
        }
    }
}
