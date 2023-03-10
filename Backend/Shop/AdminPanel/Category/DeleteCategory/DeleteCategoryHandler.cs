using MediatR;
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
            var category = _shopDbContext.Categories.Find(request.Id);

            if (category != null)
            {
                try
                {
                    var childrenCategories = _shopDbContext.Categories.Where(c => c.ParentCategoryId == category.Id).ToList();

                    foreach(var childCategory in childrenCategories)
                    {
                        childCategory.ParentCategory = category.ParentCategory;
                    }

                    _shopDbContext.SaveChanges();

                    _shopDbContext.Categories.Remove(category);
                    _shopDbContext.SaveChanges();
                    return $"Category '{category.Name}' was removed";
                }
                catch (Exception ex)
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
