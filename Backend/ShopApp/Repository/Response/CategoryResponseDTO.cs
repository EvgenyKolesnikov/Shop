using Newtonsoft.Json;
using Shop.Model;
using ShopApp.Common;

namespace Shop.Repository.Response
{
    public class CategoryResponseDTO : Entity
    {
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }


        public virtual List<Feature> Features { get; set; }


        public CategoryResponseDTO(Category category) 
        {
            Name = category.Name;
            Id = category.Id;
            ParentCategoryId = category.ParentCategoryId;


            Features = Tree.GetParentFeatures(category);
        }
    }
}
