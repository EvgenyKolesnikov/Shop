using Shop.Model;


namespace Shop.Repository
{
    public class MockProductRepository : IProductRepository<Product>
    {
        public IEnumerable<Product> GetList()
        {
            return TestData.Products; 
        }

        public Product GetById(int Id)
        {
            return TestData.Products.First(i => i.Id == Id);
        }

        public void Create(Product product)
        {
            TestData.Products.Add(product);
        }

        public void Update(Product product)
        {
            var item = TestData.Products.FirstOrDefault(i => i.Id == product.Id);
     
            item.Name = product.Name;
            item.Price = product.Price;
            item.CategoryId = product.CategoryId;
        }

        public void Delete(Product product)
        {
            var item = TestData.Products.FirstOrDefault(i => i.Id == product.Id);
            TestData.Products.Remove(item);
        }
    }
}
