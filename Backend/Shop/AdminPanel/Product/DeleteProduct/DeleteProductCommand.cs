using MediatR;

namespace Shop.AdminPanel.DeleteProduct
{
    public class DeleteProductCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
