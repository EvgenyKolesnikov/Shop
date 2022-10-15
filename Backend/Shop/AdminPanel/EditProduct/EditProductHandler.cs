using MediatR;
using Shop.AdminPanel.GetCategoryFeatures;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, string>
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IMediator _mediator;

        public EditProductHandler(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        public async Task<string> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            string result = "";

            var product = _shopDbContext.Products.FirstOrDefault(i => i.Id == command.ProductId);
            var features = await _mediator.Send(new GetCategoryFeaturesQuery() { Id = product.CategoryId });


            if (product == null)
            {
                return "Product doesn't exist";
            }

            if (command.Name != null) { product.Name = command.Name; result += "Name has been changed \n"; }
            if (command.Price != null) { product.Price = command.Price; result += "Price has been changed \n"; }
            if (command.Info != null) { product.Info = command.Info; result += "Info has been changed \n"; };
            if (command.Rating != null) { product.Rating = command.Rating; result += "Rating has been changed \n"; }
            if (command.CategoryId != null)
            {
                var category = _shopDbContext.Categories.Find(command.CategoryId);
                if (category != null)
                {

                    product.Category = category;
                    product.CategoryId = command.CategoryId;


                    // перезаписывание фичей
                    product.Features.RemoveAll(i => product.Features.Contains(i));
                    foreach (var feature in category.Features)
                    {
                        var value = new FeatureValue
                        {
                            Feature = feature,
                            Product = product,
                        };
                        product.Features.Add(value);
                    }

                    result += "Category has been changed \n";
                }
                else
                {
                    result += "Category doesn't exist !!!";
                }
            }
            
            foreach (var feature in features ?? new List<Feature>())
            {
                var item = product.Features.FirstOrDefault(i => i.FeatureId == feature.Id);

                //add feature
                if (item == null)
                {
                    var featureitem = new FeatureValue()
                    {
                        Feature = feature,
                        Product = product,
                        
                    };
                    foreach (var i in command.FeatureValue)
                    {
                        if (i.Key.Equals(feature.Id))
                        {
                            featureitem.Value = i.Value;
                        }
                    }

                    await _shopDbContext.AddAsync(featureitem);
                }
                else
                {
                    foreach (var i in command.FeatureValue)
                    {
                        if (i.Key.Equals(item.FeatureId))
                        {
                            item.Value = i.Value;
                        }
                    }
                }
            }


            await _shopDbContext.SaveChangesAsync();

            return result;
        }
    }
}
