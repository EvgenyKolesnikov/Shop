using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Model;
using ShopApp.Repository.Response;

namespace ShopApp.Repository.Products
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly ShopDbContext _shopDbContext;
        public ProductRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public IQueryable<Product> GetList(ProductFilterPrompt filter)
        {
            var query = _shopDbContext.Products.AsQueryable();

            var products = ProductFilter.Filter(query, filter);


            return products;
        }

        public Pagination<Product> GetProductsWithPagination(int pageSize, int pageIndex)
        {
            var products = _shopDbContext.Products.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var response = new Pagination<Product>(products, pageSize, pageIndex);


            return response;
        }


        public async Task<Product> GetById(int Id)
        {
            var product = await _shopDbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);


            return product;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            var features = _shopDbContext.Features;

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

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory(int id)
        {
            var products = await _shopDbContext.Categories.FindAsync(id);

            List<ProductResponseDTO> result = new List<ProductResponseDTO>();

            foreach (var product in products?.Products ?? new List<Product>())
            {
                result.Add(new ProductResponseDTO(product));
            }

            return result;
        }


    }
}
