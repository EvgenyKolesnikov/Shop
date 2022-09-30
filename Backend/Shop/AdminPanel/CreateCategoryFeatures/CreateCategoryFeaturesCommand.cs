using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Shop.AdminPanel.CreateCategoryFeatures
{
    public class CreateCategoryFeaturesCommand : IRequest<int>
    {
        [Required]
        public int CategoryId { get; set; }
        public string? FeatureName { get; set; }
    }
}
