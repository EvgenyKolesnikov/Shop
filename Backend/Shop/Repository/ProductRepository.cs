using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Model;
using Shop.Repository.Response;

namespace Shop.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly ShopDbContext _shopDbContext;
        public ProductRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public IEnumerable<Product> GetList()
        {
            var products = _shopDbContext.Products;

            return products;
        }

        public Product GetById(int Id)
        {
            var product = _shopDbContext.Products.FirstOrDefault(x => x.Id == Id);


            return product;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            var features = _shopDbContext.Features.ToList();

            return features;
        }

        public IEnumerable<Category> GetCategoriesTree()
        {

            var categories = _shopDbContext.Categories.Where(c => c.ParentCategoryId == null).AsParallel().ToList();
            
            return categories; 
        }

        public IEnumerable<Category> GetCategories()
        {

            var categories = _shopDbContext.Categories.ToList();

            return categories;
        }
    }
}
