using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.Model;

namespace Shop.AdminPanel.EditFeature
{
    public class EditFeatureCommand : IRequest<FeaturesResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }


    }
}
