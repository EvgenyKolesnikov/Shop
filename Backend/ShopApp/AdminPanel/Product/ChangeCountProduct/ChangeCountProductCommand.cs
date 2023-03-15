using MediatR;

namespace Shop.AdminPanel.ChangeCountProduct
{
    public class ChangeCountProductCommand : IRequest<ProductResponse>
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
