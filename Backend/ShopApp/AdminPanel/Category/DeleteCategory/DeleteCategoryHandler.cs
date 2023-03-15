using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.AdminPanel.DeleteProduct;
using Shop.Database;

namespace Shop.AdminPanel.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, string>
    {
    
        private readonly ShopDbContext _shopDbContext;

        public DeleteCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _shopDbContext.Categories.FindAsync(request.Id);

            if (category != null)
            {
                try
                {
                    var childrenCategories = await _shopDbContext.Categories.Where(c => c.ParentCategoryId == category.Id).ToListAsync();

                    foreach(var childCategory in childrenCategories)
                    {
                        childCategory.ParentCategory = category.ParentCategory;
                    }

                    await _shopDbContext.SaveChangesAsync();

                    _shopDbContext.Categories.Remove(category);
                    await _shopDbContext.SaveChangesAsync();
                    return $"Category '{category.Name}' was removed";
                }
                catch 
                {
                    return $"Category '{category.Name}' was removed";
                }
            }
            else
            {
                return "Category Not Found";
            }

        }
    }
}
