using System;
using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    //same as basket.api.entities.shoppingcart
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();
        public decimal TotalPrice { get; set; }
    }
}
