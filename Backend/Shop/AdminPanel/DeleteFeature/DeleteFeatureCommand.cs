using MediatR;

namespace Shop.AdminPanel.DeleteFeature
{
    public class DeleteFeatureCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
