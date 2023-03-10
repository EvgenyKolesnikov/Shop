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
            var editCategory = await _shopDbContext.Categories.FindAsync(command.CategoryId);

            if (editCategory == null) return null;

            editCategory.Name = command.Name;

            if (command.ParentCategoryId != null && command.ParentCategoryId != 0)
            {
                var newParentCategory = await _shopDbContext.Categories.FindAsync(command.ParentCategoryId);
                var childrenCategories = editCategory.ChildCategories.ToList();

                
                foreach (var childCategory in childrenCategories)
                {
                    childCategory.ParentCategory = editCategory.ParentCategory;
                }

                editCategory.ParentCategory = newParentCategory;
                editCategory.ParentCategoryId = newParentCategory.Id;
            }


            await _shopDbContext.SaveChangesAsync();

            return editCategory;
        }
    }
}
