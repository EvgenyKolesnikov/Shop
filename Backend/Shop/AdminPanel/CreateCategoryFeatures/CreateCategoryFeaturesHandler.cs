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


            // Ищем категорию
            var category = _shopDbContext.Categories.Find(command.CategoryId);
            if (category == null)
            {
                response.result = "Category not found";
                return response;
            }

            //Ищем фичу в списке
            var existFeature = _shopDbContext.Features.FirstOrDefault(i => i.Name.ToLower() == command.Name.ToLower());

            if(existFeature != null)
            {
                var existCategory = existFeature.Categories.FirstOrDefault(i => i.Id == command.CategoryId);

                if (existCategory != null)
                {
                    response.Id = existCategory.Id;
                    response.result = "Feature already exist in current category";
                    return response;
                }
                else
                {
                    //Связывание фичи с другой категорией
                    existFeature.Categories.Add(category);
                    await _shopDbContext.SaveChangesAsync();

                    response.Id = existFeature.Id;
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
                

                var categories = GetAllChildsCategories(category); 

                var products = _shopDbContext.Products.Where(i => categories.Contains(i.Category)).ToList();

                foreach (var product in products)
                {
                    var featureitem = new FeatureValue()
                    {
                        Feature = feature,
                        Product = product,
                        Value = null
                    };
                    await _shopDbContext.AddAsync(featureitem);
                }
                await _shopDbContext.SaveChangesAsync();



                response.Id = feature.Id;
                response.result = "New feature has been added";
                return response;
            }
        }

        private List<Category> GetAllChildsCategories(Category category)
        {
            var result = new List<Category>();
            result.Add(category);

            for (int i = 0; i < result.Count; i++)
            {
                foreach (var a in result[i].ChildCategories ?? new List<Category>())
                {
                    result.Add(a);
                }
            };

            return result;
        }
    }
}
