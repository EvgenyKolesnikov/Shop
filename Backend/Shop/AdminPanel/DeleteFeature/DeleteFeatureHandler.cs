using MediatR;
using Shop.Database;

namespace Shop.AdminPanel.DeleteFeature
{
    public class DeleteFeatureHandler : IRequestHandler<DeleteFeatureCommand, string>
    {
        private readonly ShopDbContext _shopDbContext;

        public DeleteFeatureHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<string> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = _shopDbContext.Features.Find(request.Id);

            if (feature != null)
            {
                _shopDbContext.Features.Remove(feature);
                await _shopDbContext.SaveChangesAsync();


                return $"Feature '{feature.Name}' was removed";
            }
            else
            {
                return "Feature Not Found";
            }

        }
    }
}
