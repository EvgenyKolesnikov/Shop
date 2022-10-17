using Newtonsoft.Json;
using Shop.Model;

namespace Shop.Repository.Response
{
    public class CategoryResponseTreeDTO
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        public List<Category> ChildCategories { get; set; }

        public virtual List<FeatureResponseDTO> Features { get; set; }


        public CategoryResponseTreeDTO(Category category)
        {
            Name = category.Name;
            CategoryId = category.Id;
            ParentCategoryId = category.ParentCategoryId;
            ParentCategory = category.ParentCategory;
            
            ChildCategories = category.ChildCategories;
            Features = category.Features.Select(f => new FeatureResponseDTO()
            {
                Name = f.Name
            }).ToList();
        }
    }
}
