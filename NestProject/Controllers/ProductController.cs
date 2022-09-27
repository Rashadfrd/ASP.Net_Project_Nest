using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.ViewModels;
using Newtonsoft.Json;

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
            var products = _context.Products.Include(p => p.ProductImages).Include(x => x.Badge).Include(x => x.Category).AsQueryable();
            if (filter.CategoryId != 0 && filter.PartnerId != 0)
            {
                products = products.Where(x => x.CategoryId == filter.CategoryId && x.PartnerId == filter.PartnerId);
            }
            else
            {
                if (filter.CategoryId != 0)
                {
                    products = products.Where(x=>x.CategoryId == filter.CategoryId);
                }
                if (filter.PartnerId != 0)
                {
                    products = products.Where(x => x.PartnerId == filter.PartnerId);
                }
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
        public IActionResult AddToCart(int? id)
        {
            if (id != null) return BadRequest();
            List<BasketItem> basketItems;
            if (HttpContext.Request.Cookies["Basket"] != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItem>>(HttpContext.Request.Cookies["Basket"]);
            }
            else
            {
                basketItems = new List<BasketItem>();
            }

            var alreadyAddedPrd = basketItems.Find(x => x.ProductId == id);
            if (alreadyAddedPrd is null)
            {
            basketItems.Add(new BasketItem { Count = 1, ProductId = (int)id });
            }
            else
            {
                alreadyAddedPrd.Count++;
            }

            HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basketItems), new CookieOptions { MaxAge = TimeSpan.MaxValue });
            return Json(HttpContext.Request.Cookies["Basket"]);
        }
    }
}
