using Shop.Model;
using ShopApp.Repository.Response;

namespace ShopApp.Repository.Products
{
    public class GetProductsResponse
    {
        public List<ProductResponseDTO> Products { get; set; } = new();
        public int PageSize { get; }
        public int PageIndex { get; }
       
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public GetProductsResponse(Pagination<Product> page)
        {
            foreach (var product in page.Items)
            {
                Products.Add(new ProductResponseDTO(product));
            }
            
            PageSize = page.PageSize;
            PageIndex = page.PageIndex;
            TotalPages = page.TotalPages;
            TotalItems = page.TotalItems;
        }
    }
}
