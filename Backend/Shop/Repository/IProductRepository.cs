using Shop.Model;
using Shop.Repository.Response;

namespace Shop.Repository
{
    public interface IProductRepository<T> where T : class
    {
        IEnumerable<Product> GetList();
        T GetById(int Id);
        IEnumerable<Feature> GetFeatures();
        IEnumerable<Category> GetCategoriesTree();
        IEnumerable<Category> GetCategories();
        // void Create(T item);
        // void Update(T item);
        // void Delete(T item);
    }
}
