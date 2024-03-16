﻿namespace BasketApplication.Entities.Models
{
    public class AppUserBasket
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }

    }
}