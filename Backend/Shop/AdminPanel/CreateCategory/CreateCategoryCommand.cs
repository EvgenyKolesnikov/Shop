using MediatR;

namespace Shop.AdminPanel.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
