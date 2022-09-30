using Shop.Model;


namespace Shop.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        public ProductRepository()
        {
        }


        public IEnumerable<Product> GetList()
        {
            return TestData.products; 
        }

        public Product GetById(int Id)
        {
            return TestData.products.First(i => i.Id == Id);
        }

        public void Create(Product product)
        {
            TestData.products.Add(product);
        }

        public void Update(Product product)
        {
            var item = TestData.products.FirstOrDefault(i => i.Id == product.Id);
     
            item.Name = product.Name;
            item.Price = product.Price;
            item.CategoryId = product.CategoryId;
        }

        public void Delete(Product product)
        {
            var item = TestData.products.FirstOrDefault(i => i.Id == product.Id);
            TestData.products.Remove(item);
        }
    }
}
