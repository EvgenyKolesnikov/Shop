using Shop.Model;
using ShopApp.Repository.Response;

namespace ShopApp.Repository.Products
{
    public interface IProductRepository<T> where T : class
    {
        IQueryable<Product> GetList(ProductFilterPrompt filter);

        Pagination<Product> GetProductsWithPagination(int pageSize, int pageIndex);
        Task<T> GetById(int Id);
        IEnumerable<Feature> GetFeatures();
        IEnumerable<Category> GetCategoriesTree();
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory(int categoryId);
    }
}
