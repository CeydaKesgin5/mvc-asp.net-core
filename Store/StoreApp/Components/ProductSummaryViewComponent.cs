using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ProductSummaryViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }



        public string Invoke()
        {
            //repository
            // return _context.Products.Count().ToString();

            //manager
            return _manager.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
}
