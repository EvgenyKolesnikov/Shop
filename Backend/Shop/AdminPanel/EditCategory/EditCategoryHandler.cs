using MediatR;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.EditCategory
{
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, Category>
    {
        private readonly ShopDbContext _shopDbContext;

        public EditCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<Category> Handle(EditCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _shopDbContext.Categories.FindAsync(command.CategoryId);

            if (category == null) return null;

            category.Name = command.Name;

            if (command.ParentCategoryId != null && command.ParentCategoryId != 0)
            {
                var parentCategory = await _shopDbContext.Categories.FindAsync(command.ParentCategoryId);

                category.ParentCategory = parentCategory;
                category.ParentCategoryId = command.ParentCategoryId;
            }

           

            await _shopDbContext.SaveChangesAsync();

            return category;
        }
    }
}
