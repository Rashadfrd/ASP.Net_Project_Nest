using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nesting.ViewModels;
using NestProject.DAL;

namespace NestProject.Controllers
{
    public class HomeController : Controller
    {
        private NestContext _context { get; set; }
        public HomeController(NestContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.Sliders = _context.Sliders.OrderBy(x => x.Order);
            vm.Categories = _context.Categories.Include(x => x.Products);
            vm.Products = _context.Products.Include(x => x.Badge).Include(x=>x.ProductImages);
            return View(vm);
        }
    }
}
