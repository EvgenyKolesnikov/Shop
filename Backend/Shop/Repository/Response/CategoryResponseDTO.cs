using Shop.Model;

namespace Shop.Repository.Response
{
    public class CategoryResponseDTO
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual List<FeatureResponseDTO> Features { get; set; }


        public CategoryResponseDTO(Category category)
        {
            Name = category.Name;
            CategoryId = category.Id;
            ParentCategoryId = category.ParentCategoryId;

            Features = category.Features.Select(f => new FeatureResponseDTO()
            {
                Name = f.Name
            }).ToList();
        }
    }
}
