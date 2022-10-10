using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand,string>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<string> Handle(CreateCategoryCommand command,CancellationToken cancellationToken)
        {
            var ParentCategory = _shopDbContext.Categories.Find(command.ParentCategoryId); 

                var category = new Category()
                {
                    Name = command.Name,
                    ParentCategoryId = command.ParentCategoryId == 0 ? null : command.ParentCategoryId
                };

           


            await _shopDbContext.Categories.AddAsync(category);
            await _shopDbContext.SaveChangesAsync();


            return $"'{category.Name}' was added";
        }
    }
}
