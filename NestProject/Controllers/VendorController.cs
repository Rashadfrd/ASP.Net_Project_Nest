using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nesting.ViewModels;
using NestProject.DAL;
using NestProject.Models;
using NestProject.ViewModels;
using System.Linq;

namespace Nesting.Controllers
{
    public class VendorController : Controller
    {
        private NestContext _context { get; }
        public VendorController(NestContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var partners = _context.Partners.Include(x=>x.Products);
            return View(partners);
        }

        public IActionResult Detail(int? id)
        {
            if (id is null) return RedirectToAction(nameof(Index));
            VendorVM vm = new VendorVM();
            vm.Categories = _context.Categories;
            vm.Products = _context.Products.Include(x => x.Badge).Include(x => x.ProductImages).Include(x => x.Category);
            vm.Partner = _context.Partners.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }
    }
}
