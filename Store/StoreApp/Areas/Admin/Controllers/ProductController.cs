using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        }

        
        public IActionResult Create()
        {
            //Seçilebilir bir liste tanımı
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false), //veri tabanındaki kayıtlar item
             "CategoryId", //veri alanı
             "CategoryName", //text alanı
             "1"); //default olarak idsi 1 olan gelecek 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ProductDtoForInsertion productDto) {
            if (ModelState.IsValid) {
                _manager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
            
        }


        public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();

            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductDtoForUpdate productDto)
        {
            if (ModelState.IsValid)
            {
                _manager.ProductService.UpdateOneProduct(productDto);
                
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }

    }
}
