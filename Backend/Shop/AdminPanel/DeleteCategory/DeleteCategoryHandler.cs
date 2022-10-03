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
                _shopDbContext.Categories.Remove(category);
                await _shopDbContext.SaveChangesAsync();


                return $"Category '{category.Name}' was removed";
            }
            else
            {
                return "Category Not Found";
            }

        }
    }
}
