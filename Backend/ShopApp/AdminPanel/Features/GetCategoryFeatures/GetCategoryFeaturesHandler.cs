using MediatR;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.GetCategoryFeatures
{
    public class GetCategoryFeaturesHandler : IRequestHandler<GetCategoryFeaturesQuery, IEnumerable<Feature>>
    {
        private readonly ShopDbContext _shopDbContext;

        public GetCategoryFeaturesHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public async Task<IEnumerable<Feature>> Handle(GetCategoryFeaturesQuery command, CancellationToken cancellationToken)
        {
            var category = await _shopDbContext.Categories.FindAsync(command.Id);

            if (category == null) return new List<Feature>();
            var allFeatures = category?.Features.ToList();

            var ParentCategory = category?.ParentCategory;
            while(ParentCategory != null)
            {
                allFeatures?.AddRange(ParentCategory.Features.ToList());
                ParentCategory = ParentCategory.ParentCategory;

                cancellationToken.ThrowIfCancellationRequested();
            }
            

            return allFeatures ?? new List<Feature>();
        }
    }
}
