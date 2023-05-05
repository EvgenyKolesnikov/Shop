using Shop.Model;
using ShopApp.Common;

namespace ShopApp.Repository.Products
{
    public static class ProductFilter
    {
        public static IQueryable<Product> Filter (IQueryable<Product> query, ProductFilterPrompt prompt)
        {
          
            if (prompt.CategoryIds is not null)
            {
                prompt.CategoryIds.RemoveAll(i => i == null);

                if (prompt.CategoryIds.Count == 0) { return query; }

                var categories = query.Where(i => prompt.CategoryIds.Contains(i.CategoryId)).Select(i => i.Category).ToList();

                var ChildrenCategories = Tree.GetChildrenCategories(categories);

                var filterIds = ChildrenCategories.Select(c => c.Id).ToList().Cast<int?>();

                query = query.Where(i => filterIds.Contains(i.CategoryId));
            }

            if (prompt.Count is not null)
            {
                query = query.Where(i => (prompt.Count.From == null || i.Count >= prompt.Count.From) &&
                                         (prompt.Count.To == null || i.Count <= prompt.Count.To));
            }

            if (prompt.Price is not null)
            {
                query = query.Where(i => (prompt.Price.From == null || i.Price >= prompt.Price.From) &&
                                         (prompt.Price.To == null || i.Price <= prompt.Price.To));
            }

            if (prompt.Rating is not null)
            {
                query = query.Where(i => (prompt.Rating.From == null || i.Rating >= prompt.Rating.From) &&
                                         (prompt.Rating.To == null || i.Rating <= prompt.Rating.To));
            }
            return query;
        }
    }
}
