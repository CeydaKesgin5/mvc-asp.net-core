using Entities.Models;
using System.Runtime.CompilerServices;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
        {
            if(categoryId is null)
                return products;
            else 
                return products.Where(prd=>prd.CategoryId.Equals(categoryId));
        }

        public static IQueryable<Product> FilteredBySearchItem(this IQueryable<Product> products, String? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return products;
            else
                return products.Where(prd => prd.ProductName.ToLower()
                    .Contains(searchTerm.ToLower()));
        }
        
    }
}
