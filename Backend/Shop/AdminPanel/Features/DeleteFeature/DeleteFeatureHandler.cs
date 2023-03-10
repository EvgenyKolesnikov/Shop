using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.Database;

namespace Shop.AdminPanel.DeleteFeature
{
    public class DeleteFeatureHandler : IRequestHandler<DeleteFeatureCommand, FeaturesResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public DeleteFeatureHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<FeaturesResponse> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = _shopDbContext.Features.Find(request.Id);
            var response = new FeaturesResponse();

            if (feature != null)
            {
                _shopDbContext.Features.Remove(feature);
                await _shopDbContext.SaveChangesAsync();

                response.Feature = feature;
                response.result = $"Feature '{feature.Name}' was removed";
            }
            else
            {
                response.result = "Feature Not Found";
            }
            return response;
        }
    }
}
