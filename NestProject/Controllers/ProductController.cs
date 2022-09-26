using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.ViewModels;

namespace Nesting.Controllers
{
    public class ProductController : Controller
    {
        private NestContext _context { get; }
        public ProductController(NestContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId,int?partnerId)
        {
            ProductVM vm = new ProductVM();
            vm.Categories = _context.Categories.Include(x=>x.Products);
            vm.Partners = _context.Partners.Include(x=>x.Products);
            var products = _context.Products.Include(x=>x.ProductImages).Include(x => x.Badge).Include(x => x.Category).AsQueryable();
            if (categoryId != null)
            {
                products = products.Where(x => x.CategoryId == categoryId);
            }
            if (partnerId != null)
            {
                products = products.Where(x => x.PartnerId == partnerId);
            }
            vm.Products = products.ToList();
            return View(vm);
        }
        public IActionResult ProductFilter(ProductFilterVM filter)
        {
            var products = _context.Products.Include(p => p.ProductImages).AsQueryable ();
            if (filter.CategoryId != 0)
            {
                products = products.Where(x=>x.CategoryId == filter.CategoryId);
            }
            if (products is null) return NotFound();
            return PartialView("_FilterPartialView", products);
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Compare()
        {
            return View();
        }
        public IActionResult Basket()
        {
            return View();
        }
    }
}
