﻿using MediatR;
using Shop.AdminPanel.Commands;
using Shop.Database;
using Shop.Model;

namespace Shop.AdminPanel.Handlers
{
    public class LinkFeatureWithCategoryHandler : IRequestHandler<LinkFeatureWithCategoryCommand>
    {
        private readonly ShopDbContext _shopDbContext;

        public LinkFeatureWithCategoryHandler(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<Unit> Handle(LinkFeatureWithCategoryCommand request, CancellationToken cancellationToken)
        {
            var feature = _shopDbContext.Features.Find(request.FeatureId);
            if (feature == null) return Unit.Value;
            await _shopDbContext.Entry(feature).Collection(x => x.Categories).LoadAsync();
            if (feature.Categories.Any(x => x.Id == request.CategoryId)) return Unit.Value;
            var category = _shopDbContext.Categories.Find(request.CategoryId);
            if (category == null) return Unit.Value;

            feature.Categories.Add(category);

            await _shopDbContext.Entry(category).Collection(x => x.Products).LoadAsync();
            foreach (var product in category.Products)
            {
                var value = new FeatureValue
                {
                    Product = product,
                    Feature = feature,
                };
                _shopDbContext.FeatureValues.Add(value);
            }

            await _shopDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
