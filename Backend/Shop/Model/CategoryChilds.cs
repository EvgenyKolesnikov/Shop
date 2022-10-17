using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class CategoryChilds : Entity
    {
        
        public int? CategoryId { get; set; }
        public int? ChildCategoryId { get; set; }
        
        public virtual List<Category> ChildCategories { get; set; } = new List<Category>();
    }
}
