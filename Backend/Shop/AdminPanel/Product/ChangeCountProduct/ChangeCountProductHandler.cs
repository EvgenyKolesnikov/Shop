using MediatR;
using Shop.Database;

namespace Shop.AdminPanel.ChangeCountProduct
{
    public class ChangeCountProductHandler : IRequestHandler<ChangeCountProductCommand,ProductResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public ChangeCountProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<ProductResponse> Handle(ChangeCountProductCommand command,CancellationToken cancellationToken)
        {
            var product = await _shopDbContext.Products.FindAsync(command.ProductId);

            if (product == null) { return new ProductResponse() { Message = "Товар не существует" }; };

            product.Count = command.Count;

            await _shopDbContext.SaveChangesAsync(cancellationToken);

            return new ProductResponse() {Product = product, Message = "Success" };
        }

    }
}
