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
              
                if (category != null && category != product.Category)
                {
                    product.Category = category;
       
                    product.Features.RemoveAll(i => product.Features.Contains(i));   
                    result += "Category has been changed \n";
                }
                else
                {
                    result += "Category doesn't exist !!!";
                }
            }
            
            foreach (var feature in features ?? new List<Feature>())
            {
                var existFeature = product.Features.FirstOrDefault(i => i.FeatureId == feature.Id);
                var value = command.FeatureValue.FirstOrDefault(i => i.Key == feature.Id);

                //add feature
                if (existFeature == null)
                {
                    var featureitem = new FeatureValue()
                    {
                        Feature = feature,
                        Product = product,
                        Value = value.Value
                    };
                    await _shopDbContext.AddAsync(featureitem);
                }
                else
                {
                    existFeature.Value = value.Value;
                }
            }

            await _shopDbContext.SaveChangesAsync();

            return result;
        }
    }
}
