using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.Areas.Manage.ViewModel;
using NestProject.DAL;

namespace NestProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private NestContext _context { get; }
        public ProductController(NestContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AdminProductVM vm = new AdminProductVM();
            vm.Categories = _context.Categories.ToList();
            vm.Products = _context.Products.Include(x=>x.ProductImages).ToList();            
            return View(vm);
        }
    }
}
