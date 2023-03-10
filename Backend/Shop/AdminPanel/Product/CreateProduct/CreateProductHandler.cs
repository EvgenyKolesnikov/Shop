using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateProduct;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var Existproduct = await _shopDbContext.Products.FirstOrDefaultAsync(i => i.Name == command.Name);
            if(Existproduct != null) { return new ProductResponse() { Product = Existproduct, Message = "Товар уже существует"}; };
                      
            var category = await _shopDbContext.Categories.FindAsync(command.CategoryId);

            var parentsCategories = GetParents(category);
            var features = GetAllFeatures(parentsCategories);

            
            var product = new Product
            {
                Name = command.Name,
                Category = category,
                Info = command.Info,
                Price = command.Price
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
                    product.FeatureValues.Add(value);
                }
            }
            
            await _shopDbContext.Products.AddAsync(product);
            await _shopDbContext.SaveChangesAsync();

            var response = new ProductResponse() { Product = product, Message = "Success"};

            return response;
        }

        private List<Category> GetParents(Category category)
        {
            var result = new List<Category>();

            if(category == null) { return result; }
            
            result.Add(category);

            var ParentCategory = category?.ParentCategory;
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
