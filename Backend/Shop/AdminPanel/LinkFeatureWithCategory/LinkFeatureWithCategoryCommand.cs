using MediatR;

namespace Shop.AdminPanel.Commands
{
    public class LinkFeatureWithCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public int FeatureId { get; set; }
    }
}
