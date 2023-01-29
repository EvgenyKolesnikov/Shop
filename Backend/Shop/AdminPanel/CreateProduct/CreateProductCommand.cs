using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Info { get; set; }
        public float Price { get; set; }
        public float? Rating { get; set; }
    }
}
