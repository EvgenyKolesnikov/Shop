using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.GetCategoryFeatures
{
    public class GetCategoryFeaturesQuery :IRequest<IEnumerable<Feature>>
    {
        public int? Id { get; set; }
    }
}
