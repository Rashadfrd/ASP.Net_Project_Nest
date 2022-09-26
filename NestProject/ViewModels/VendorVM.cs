using NestProject.Models;

namespace NestProject.ViewModels
{
    public class VendorVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Partner Partner { get; set; }
    }
}
