using MediatR;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.EditProduct;
using Shop.Model;

namespace Shop.AdminPanel.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Info { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }

        public List<FeatureIdValue> FeatureValue { get; set; } = new();
       // public Dictionary<int, string> FeatureValue { get; set; } = new();
    }
}
