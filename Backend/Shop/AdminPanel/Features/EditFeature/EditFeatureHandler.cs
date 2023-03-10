using MediatR;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.EditFeature
{
    public class EditFeatureHandler : IRequestHandler<EditFeatureCommand, FeaturesResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public EditFeatureHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }


        public async Task<FeaturesResponse> Handle(EditFeatureCommand command, CancellationToken cancellationToken)
        {
            var feature = await _shopDbContext.Features.FindAsync(command.Id);

            if(feature == null) { return null; }
            
            feature.Name = command.Name;

            await _shopDbContext.SaveChangesAsync();

            var response = new FeaturesResponse() { Feature = feature, result = "Feature add"};

            return response;
        }
    }
}
