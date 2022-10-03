using Shop.Model;

namespace Shop.Repository.Response
{
    public class ProductResponseDTO
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
  
        public virtual List<FeatureResponseDTO> Features { get; set; }
        public string? Info { get; set; }
        public float Price { get; set; }
        public string? Rating { get; set; }


        public ProductResponseDTO(Product product)
        {
            Name = product.Name;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Name;
            Info = product.Info;
            Price = product.Price;
            Rating = product.Rating;

            Features = product.Features.Select(f => new FeatureResponseDTO()
            {
                Name = f.Feature.Name,
                Value = f.Value
            }).ToList();
        }
    }
}
