using MediatR;
using Shop.Database;

namespace Shop.AdminPanel.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly ShopDbContext _shopDbContext;

        public DeleteProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _shopDbContext.Products.FindAsync(request.Id);

            if (product != null)
            {
                _shopDbContext.Products.Remove(product);
                await _shopDbContext.SaveChangesAsync();


                return $"Product '{product.Name}' was removed";
            }
            else
            {
                return "Product Not Found";
            }

        }
    }
}
