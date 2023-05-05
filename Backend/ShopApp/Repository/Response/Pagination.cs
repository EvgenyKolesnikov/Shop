namespace ShopApp.Repository.Response
{
    public class Pagination<T>
    {

        public List<T> Items { get; } = new List<T>();


        public int PageSize { get; }
        public int PageIndex { get; }
        public int TotalItems { get; set; }

        public int TotalPages {
            get => (int)Math.Ceiling((decimal)TotalItems / PageSize);
       }


        public Pagination(IQueryable<T> items, int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalItems = items.Count();
            Items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
