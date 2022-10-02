using MediatR;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateCategoryFeaturesHandler : IRequestHandler<CreateCategoryFeaturesCommand, CreateCategoryFeaturesResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateCategoryFeaturesHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<CreateCategoryFeaturesResponse> Handle(CreateCategoryFeaturesCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateCategoryFeaturesResponse();
            var category = _shopDbContext.Categories.Find(command.CategoryId);

            if (category == null)
            {
                response.result = "Category not found";
                return response;
            }

            var existFeature = _shopDbContext.Features.FirstOrDefault(i => i.Name == command.Name);

            if(existFeature != null)
            {
                await _shopDbContext.Entry(existFeature).Collection(x => x.Categories).LoadAsync();
                var existCategory = existFeature.Categories.FirstOrDefault(i => i.Id == command.CategoryId);

                if (existCategory != null)
                {
                    response.result = "Feature already exist in current category";
                    return response;
                }
                else
                {
                    //Связывание фичи с другой категорией
                    existFeature.Categories.Add(category);
                    await _shopDbContext.SaveChangesAsync();

                    response.result = "Feature was linked with current category";
                    return response;
                }
            }
            else
            {
                //добавление новой фичи
                var feature = new Feature();
                feature.Name = command.Name;

                feature.Categories.Add(category);

                await _shopDbContext.AddAsync(feature);
                await _shopDbContext.SaveChangesAsync();


                response.result = "New feature has been added";
                return response;
            }
        }
    }
}
