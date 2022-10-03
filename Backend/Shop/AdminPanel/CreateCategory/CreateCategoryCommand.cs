using MediatR;

namespace Shop.AdminPanel.Commands
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
