using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;

namespace Shop.AdminPanel.DeleteFeature
{
    public class DeleteFeatureCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
