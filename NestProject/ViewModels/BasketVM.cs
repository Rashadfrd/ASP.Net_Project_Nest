﻿namespace NestProject.ViewModels
{
    public class BasketVM
    {
        public List<ProductBasketItemVM> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
