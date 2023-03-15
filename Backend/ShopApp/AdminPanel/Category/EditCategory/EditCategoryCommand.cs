using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.EditCategory
{
    public class EditCategoryCommand : IRequest<CategoryResponse>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
