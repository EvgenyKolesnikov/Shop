using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;
using System.ComponentModel.DataAnnotations;

namespace Shop.AdminPanel.Commands
{
    public class CreateCategoryFeaturesCommand : IRequest<CreateCategoryFeaturesResponse>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
