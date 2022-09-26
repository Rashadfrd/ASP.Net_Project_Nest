using Microsoft.EntityFrameworkCore;
using NestProject.Models;

namespace NestProject.DAL
{
    public class NestContext:DbContext
    {
        public NestContext(DbContextOptions<NestContext> opt):base(opt)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Partner> Partners { get; set; }
    }
}
