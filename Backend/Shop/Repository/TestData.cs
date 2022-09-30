using Shop.Model;

namespace Shop.Repository
{
    public static class TestData
    {
        public static List<Product> products = new List<Product>()
            {
                new Product { Id = 1, Name = "RedBook", CategoryId = 1,Price = 123 },
                new Product { Id = 2, Name = "GreenBook", CategoryId = 1, Price = 200},
                new Product { Id = 3, Name = "BlueTable", CategoryId = 2, Price = 1500 },
                new Product { Id = 4, Name = "YellowTable", CategoryId = 2, Price = 2000}
            };
    }
}
