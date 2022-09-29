using Microsoft.AspNetCore.Mvc;
using NestProject.DAL;

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
        public IActionResult Delete()
        {
            return View();
        }
    }
}
