using System.ComponentModel;
using ShopApp.Common;

namespace ShopApp.Repository.Products
{
    public class ProductFilterPrompt
    {
        public Prompt Price { get; set; }
        public Prompt Rating { get; set; }
        public Prompt Count { get; set; }
        public List<int?> CategoryIds { get; set; }     

    }
}
