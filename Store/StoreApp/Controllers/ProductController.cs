using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

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

        public IActionResult Index(){
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        } 

        public IActionResult Get([FromRoute(Name ="id")]int id){
            //Product product=_context.Products.First(p=>p.ProductId.Equals(id));
            //return View(product);
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }

    }
} 