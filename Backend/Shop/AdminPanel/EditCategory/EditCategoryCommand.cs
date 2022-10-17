using MediatR;

namespace Shop.AdminPanel.EditCategory
{
    public class EditCategoryCommand : IRequest<int>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }



    }
}
