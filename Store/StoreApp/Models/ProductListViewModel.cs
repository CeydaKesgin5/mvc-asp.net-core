using Entities.Models;

namespace StoreApp.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products =Enumerable.Empty<Product>();
        public Pagination Pagination { get; set; } = new();
        public int TotalCount =>Products.Count();
    }
}
