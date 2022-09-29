using Microsoft.AspNetCore.Mvc;
using NestProject.DAL;
using NestProject.Models;

namespace NestProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private NestContext _context { get; }
        public SliderController(NestContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var sliders = _context.Sliders.OrderBy(x=>x.Order);
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            slider.ImageUrl = " ";
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
