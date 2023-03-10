﻿using MediatR;
using Shop.AdminPanel.GetCategoryFeatures;
using Shop.Database;
using Shop.Model;
using System.Linq;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, ProductResponse>
    {
        private readonly ShopDbContext _shopDbContext;

        public EditProductHandler(ShopDbContext shopDbContext, IMediator mediator)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<ProductResponse> Handle(EditProductCommand command, CancellationToken cancellationToken)
        {
            var product = _shopDbContext.Products.FirstOrDefault(i => i.Id == command.ProductId);
            var features = product?.Category?.Features;

            if (product == null) { return new ProductResponse() { Message = "Товар отсутствует"};}

            product.Name = command.Name;
            product.Price = command.Price;
            product.Info = command.Info;

            // если меняеются категории, то набор FeatureValue удаляется
            if (command.CategoryId != null)
            {
                var newCategory = await _shopDbContext.Categories.FindAsync(command.CategoryId);
              
                if (newCategory != product.Category)
                {
                    product.Category = newCategory;
                    product.FeatureValues.RemoveAll(i => product.FeatureValues.Contains(i));
                    features = newCategory.Features;
                }
            }
            
            //создание нового набора FeatureValues, либо редактирование существующего
            foreach (var feature in features ?? new List<Feature>())
            {
                var existFeatureValue = product.FeatureValues.FirstOrDefault(i => i.FeatureId == feature.Id);
                var value = command.FeatureValue.FirstOrDefault(i => i.Key == feature.Id);

                //add feature
                if (existFeatureValue == null)
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
                    existFeatureValue.Value = value.Value;
                }
            }

            await _shopDbContext.SaveChangesAsync();

            return new ProductResponse() { Product = product, Message = "Success"};
        }
    }
}
