using MediatR;
using Shop.Database;

namespace Shop.AdminPanel.EditCategory
{
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, int>
    {
        private readonly ShopDbContext _shopDbContext;

        public EditCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<int> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = _shopDbContext.Categories.Find(command.CategoryId);





            return 1;
        }
    }
}
