using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.EditCategory
{
    public class EditCategoryCommand : IRequest<Category>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
