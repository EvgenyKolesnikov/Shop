using MediatR;

namespace Shop.AdminPanel.Commands
{
    public class CreateProductCommand : IRequest<string>
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? Info { get; set; }
        public float Price { get; set; }
        public string? Rating { get; set; }
    }
}
