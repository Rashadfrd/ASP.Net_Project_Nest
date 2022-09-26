using NestProject.Models;

namespace NestProject.ViewModels
{
    public class ProductVM
    {
       public IEnumerable<Product> Products { get; set; }
       public IEnumerable<Category> Categories{ get; set; }
       public IEnumerable<Partner> Partners{ get; set; }
    }
}
