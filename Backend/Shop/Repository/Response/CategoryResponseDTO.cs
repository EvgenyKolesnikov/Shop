using Newtonsoft.Json;
using Shop.Model;

namespace Shop.Repository.Response
{
    public class CategoryResponseDTO : Entity
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }


        public virtual List<FeatureResponseDTO> Features { get; set; }


        public CategoryResponseDTO(Category category) 
        {
            Name = category.Name;
            Id = category.Id;
            ParentCategoryId = category.ParentCategoryId;
            
            Features = category.Features.Select(f => new FeatureResponseDTO()
            {
                FeatureId = f.Id,
                Name = f.Name
            }).ToList();
        }
    }
}
