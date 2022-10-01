using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.Models;
using NestProject.ViewModels;
using Newtonsoft.Json;

namespace NestProject.Services
{
    public class LayoutService
    {
        NestContext _context { get; }
        IHttpContextAccessor _accessor { get; }
        public LayoutService(NestContext context,IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;

        }


        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(p => p.Key, p => p.Value);
        }

        public BasketVM GetBasket()
        {
            var basket = new BasketVM { Products = new(), TotalPrice = 0 };
            var basketItems = new List<BasketItem>();
            string cookie = _accessor.HttpContext.Request.Cookies["Basket"];
            if (cookie != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItem>>(cookie);
            }
            foreach (var item in basketItems)
            {
                Product p = _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == item.ProductId);
                if (p != null)
                {
                    basket.Products.Add(new ProductBasketItemVM
                    {
                        Product = p,
                        Count = item.Count
                    });
                    basket.TotalPrice += p.InitialPrice * (100 - p.DiscountPercent) / 100 * item.Count;
                }
            }
            return basket;
        }
    }
}
