using Shop.Model;

namespace Shop.Repository
{
    public interface IProductRepository<T> where T : class
    {
        IEnumerable<Product> GetList();
        T GetById(int Id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
