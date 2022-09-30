using MediatR;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.CreateCategoryFeatures
{
    public class CreateCategoryFeaturesHandler : IRequestHandler<CreateCategoryFeaturesCommand, int>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateCategoryFeaturesHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<int> Handle(CreateCategoryFeaturesCommand command, CancellationToken cancellationToken)
        {
            var feature = new Feature()
            {
                CategoryId = command.CategoryId,
                FeatureName = command.FeatureName
            };

            await _shopDbContext.AddAsync(feature);
            await _shopDbContext.SaveChangesAsync();


            return feature.FeatureId;
        }
    }
}
