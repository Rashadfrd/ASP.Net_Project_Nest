using NestProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NestProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReviewSum { get; set; }
        public int ReviewCount { get; set; }
        public decimal InitialPrice { get; set; }
        [Range(0,100)]
        public decimal DiscountPercent { get; set; }
        public int Lifetime { get; set; }
        public int StockCount { get; set; }
        public string Barcode{ get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BadgeId { get; set; }
        public Badge Badge { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
