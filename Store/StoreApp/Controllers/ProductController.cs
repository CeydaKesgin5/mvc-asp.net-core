using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers{
    public class ProductController :Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        //DI

        //public IEnumerable<Product> Index1(){ 
            // var context= new RepositoryContext(
            //         new DbContextOptionsBuilder<RepositoryContext>()
            //         .UseSqlite(" Data Source  = c:\\Users\\CeydaK\\Desktop\\zy-mvc-asp.net-core\\mvc-asp.net-core\\ProductDB.db")
            //         .Options);
                
            // return context.Products; 
          //  return _context.Products; 
        //}

        public IActionResult Index(ProductRequestParameters p){

            //var model = _manager.ProductService.GetAllProductsWithDetails(p);
            //return View(model);
            var products = _manager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrenPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };

            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        } 

        public IActionResult Get([FromRoute(Name ="id")]int id){
            //Product product=_context.Products.First(p=>p.ProductId.Equals(id));
            //return View(product);
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }

    }
} 