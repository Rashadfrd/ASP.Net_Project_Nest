using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl{ get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime? ModifiedTime { get; set; }
        public ICollection<Product>Products { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
