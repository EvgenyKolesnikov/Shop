using MediatR;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, string>
    {
        private readonly ShopDbContext _shopDbContext;

        public EditProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<string> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            string result = "";

            var product = _shopDbContext.Products.FirstOrDefault(i => i.Id == command.ProductId);

            if (product == null)
            {
                return "Product doesn't exist";
            }

            if(command.Name != null) { product.Name = command.Name; result += "Name has been changed \n"; }
            if(command.Price != null) { product.Price = command.Price; result += "Price has been changed \n"; }
            if(command.Info != null) { product.Info = command.Info; result += "Info has been changed \n"; };
            if(command.Rating != null) { product.Rating = command.Rating; result += "Rating has been changed \n"; }
            if(command.CategoryId != null) 
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
            
            await _shopDbContext.SaveChangesAsync();

            return result;
        }
    }
}
