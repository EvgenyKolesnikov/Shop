using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Shop.AdminPanel.Commands
{
    public class CreateCategoryFeaturesCommand : IRequest<int>
    {
        public string? Name { get; set; }
    }
}
