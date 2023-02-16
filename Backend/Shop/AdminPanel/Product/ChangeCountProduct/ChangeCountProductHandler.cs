using MediatR;
using Shop.Database;

namespace Shop.AdminPanel.ChangeCountProduct
{
    public class ChangeCountProductHandler : IRequestHandler<ChangeCountProductCommand,ChangeCountProductResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public ChangeCountProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<ChangeCountProductResponse> Handle(ChangeCountProductCommand command,CancellationToken cancellationToken)
        {
            var product = await _shopDbContext.Products.FindAsync(command.ProductId);

            if (product == null) { return new ChangeCountProductResponse() { Message = "Товар не существует" }; };

            product.Count = command.Count;

            await _shopDbContext.SaveChangesAsync(cancellationToken);

            return new ChangeCountProductResponse() {Product = product, Message = "Success" };
        }

    }
}
