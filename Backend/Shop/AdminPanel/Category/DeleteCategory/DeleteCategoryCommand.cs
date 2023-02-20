using MediatR;

namespace Shop.AdminPanel.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
