using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
           List<Feature> features = new List<Feature>();

            foreach(var feature in command.Features ?? new List<string>())
            {
                features.Add(new Feature() { Name = feature });
            }
            
            var category = new Category()
            {
                Name = command.Name,
                ParentCategoryId = command.ParentCategoryId == 0 ? null : command.ParentCategoryId,
                Features = features
            };
            

            await _shopDbContext.Categories.AddAsync(category);
            await _shopDbContext.SaveChangesAsync();

            var response = new CategoryResponse() 
            { 
                Category = category,
                Message = "Success"
            };

            return response;
        }
    }
}
