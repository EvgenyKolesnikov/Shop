using Shop.Model;

namespace Shop.Repository
{
    public static class TestData
    {
        public static List<Product> products = new List<Product>()
            {
                new Product { Id = 1, Name = "RedBook", Category = 1,Price = 123 },
                new Product { Id = 2, Name = "GreenBook", Category = 1, Price = 200},
                new Product { Id = 3, Name = "BlueTable", Category = 2, Price = 1500 },
                new Product { Id = 4, Name = "YellowTable", Category = 2, Price = 2000}
            };
    }
}
