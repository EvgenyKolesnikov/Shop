using MediatR;
using Shop.AdminPanel.GetCategoryFeatures;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, EditProductResponse>
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IMediator _mediator;

        public EditProductHandler(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
            _mediator = mediator;
        }

        public async Task<EditProductResponse> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            var product = _shopDbContext.Products.FirstOrDefault(i => i.Id == command.ProductId);
            var features = await _mediator.Send(new GetCategoryFeaturesQuery() { Id = product.CategoryId });

            if (product == null)
            {
                return new EditProductResponse() { Message = "Товар отсутствует"};
            }


            if (command.CategoryId != null)
            {
                var commandCategory = await _shopDbContext.Categories.FindAsync(command.CategoryId);
              
                if (commandCategory != product.Category)
                {
                    product.Category = commandCategory;
       
                    product.Features.RemoveAll(i => product.Features.Contains(i));   
            
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

            return new EditProductResponse() { Product = product, Message = "Success"};
        }
    }
}
