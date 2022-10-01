﻿using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ShopDbContext _shopDbContext;

        public CreateProductHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = _shopDbContext.Categories.Find(request.CategoryId);
            if (category == null) throw new Exception($"Category with id={request.CategoryId} not finded");
            var product = new Product
            {
                Name = request.Name,
                Category = category,
                Info = request.Info,
                Price = request.Price,
                Rating = request.Rating,
            };
            foreach (var feature in category.Features) 
            {
                var value = new FeatureValue
                {
                    Feature = feature,
                    Product = product,
                };
                product.Features.Add(value);
            }
            await _shopDbContext.Products.AddAsync(product);
            await _shopDbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}