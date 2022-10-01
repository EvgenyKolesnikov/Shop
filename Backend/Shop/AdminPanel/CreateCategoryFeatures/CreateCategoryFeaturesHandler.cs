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
                Name = command.FeatureName,
                //TODO фича должна связывать с категорией в отдельном методе, т.к. ее можно создавать и без категории
                //при связывании с категорией, нужно для всех продуктов этой категории создать пустые фича-значения
            };

            await _shopDbContext.AddAsync(feature);
            await _shopDbContext.SaveChangesAsync();

            return feature.Id;
        }
    }
}
