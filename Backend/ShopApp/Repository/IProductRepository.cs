using Shop.Model;
using Shop.Repository.Response;

namespace Shop.Repository
{
    public interface IProductRepository<T> where T : class
    {
        IEnumerable<Product> GetList();
        Task<T> GetById(int Id);
        IEnumerable<Feature> GetFeatures();
        IEnumerable<Category> GetCategoriesTree();
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory (int categoryId);
    }
}
