using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.Models;

namespace NestProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private NestContext _context;
        public CategoryController(NestContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.Include(x=>x.Products);
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            category.ImageUrl = " ";            
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category.IsDeleted == false)
            {
                category.IsDeleted = true;
            }
            else
            {
            _context.Categories.Remove(category);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
