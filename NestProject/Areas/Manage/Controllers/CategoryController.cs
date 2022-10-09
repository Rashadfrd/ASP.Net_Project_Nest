using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.Models;
using NestProject.Utilities.Constants;
using NestProject.Utilities.Extensions;

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
            if (category.ImageFile is null) ModelState.AddModelError("ImageFile", "Zehmet olmasa fayl secin");
            if (!ModelState.IsValid) return View();
            var file = category.ImageFile;
            if (!file.CheckFileExtension("image/"))
            {
                ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (file.CheckFileSize(2))
            {
                ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                return View();
            }
            string newFileName = Guid.NewGuid().ToString();
            newFileName += file.CutFileName(60);
            file.SaveFile(Path.Combine("imgs", "shop", newFileName));
            category.ImageUrl = newFileName;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var category = _context.Categories.Include(x=>x.Products).FirstOrDefault(x=>x.Id == id);
            if (category is null) return NotFound("Kateqoriya tapilmadi");
            if (category.Products.Count > 0) return BadRequest("Bu kateqoriyaya aid productlar movcuddur");
            if (category.IsDeleted == false)
            {
                category.IsDeleted = true;
            }
            else
            {
                RemoveFile(Path.Combine("shop", category.ImageUrl));
                _context.Categories.Remove(category);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(int? id, Category category)
        {
            if (id != category.Id || id is null) return BadRequest();
            Category cat = _context.Categories.Find(id);
            if (cat is null) return NotFound();
            if (category.ImageFile != null)
            {
                IFormFile file = category.ImageFile;
                if (!file.CheckFileExtension("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl şəkil deyil");
                    return View();
                }
                if (file.CheckFileSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                    return View();
                }
                string newFileName = Guid.NewGuid().ToString();
                newFileName += file.CutFileName();
                RemoveFile(Path.Combine("imgs", cat.ImageUrl));
                file.SaveFile(Path.Combine("imgs", "shop", newFileName));
                cat.ImageUrl = newFileName;
            }
            cat.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public static void RemoveFile(string path)
        {
            path = Path.Combine(Constants.RootPath, "img", path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
