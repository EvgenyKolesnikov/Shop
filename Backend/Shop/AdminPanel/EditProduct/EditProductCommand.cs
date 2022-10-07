using MediatR;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; } 

        public string? Info { get; set; }

        public float? Price { get; set; }

        public int? Rating { get; set; }

    }
}
