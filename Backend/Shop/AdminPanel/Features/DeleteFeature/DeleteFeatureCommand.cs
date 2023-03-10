using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;

namespace Shop.AdminPanel.DeleteFeature
{
    public class DeleteFeatureCommand : IRequest<FeaturesResponse>
    {
        public int Id { get; set; }
    }
}
