using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
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
                Name = command.Name
            };

            await _shopDbContext.AddAsync(feature);
            await _shopDbContext.SaveChangesAsync();

            return feature.Id;
        }
    }
}
