using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestProject.Models
{
    public class Partner
    {
        public int Id { get; set; }
        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int FoundedYear { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(45)]
        public string Number { get; set; }

        [StringLength(45)]
        public string Address { get; set; }
        public string? BgImageUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Product>? Products { get; set; }

        [NotMapped]
        public IFormFile? ImageFileProfile { get; set; }

        [NotMapped]
        public IFormFile? ImageFileBg { get; set; }

        //[NotMapped]
        //public IFormFile ImageFileProfile { get; set; }
    }
}
