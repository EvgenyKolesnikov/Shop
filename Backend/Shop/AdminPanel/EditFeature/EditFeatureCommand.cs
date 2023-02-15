using MediatR;
using Shop.Model;

namespace Shop.AdminPanel.EditFeature
{
    public class EditFeatureCommand : IRequest<Feature>
    {
        public int Id { get; set; }
        public string? Name { get; set; }


    }
}
