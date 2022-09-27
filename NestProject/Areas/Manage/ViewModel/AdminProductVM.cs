using NestProject.Models;

namespace NestProject.Areas.Manage.ViewModel
{
    public class AdminProductVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
