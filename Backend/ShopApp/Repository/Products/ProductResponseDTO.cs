using Shop.Model;
using Shop.Repository.Response;

namespace ShopApp.Repository.Products
{
    public class ProductResponseDTO : Entity
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual List<FeatureResponseDTO> Features { get; set; }
        public string? Info { get; set; }
        public float? Price { get; set; }
        public float? Rating { get; set; }


        public ProductResponseDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            CategoryId = product.CategoryId;
            CategoryName = product.Category?.Name;
            Info = product.Info;
            Price = product.Price;
            Rating = product.Rating;

            Features = product.FeatureValues.Select(f => new FeatureResponseDTO()
            {
                FeatureId = f.FeatureId,
                Name = f.Feature?.Name,
                Value = f.Value
            }).ToList();
        }
    }
}
