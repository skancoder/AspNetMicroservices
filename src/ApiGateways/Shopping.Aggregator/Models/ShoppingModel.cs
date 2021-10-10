using System;
using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    //root model
    public class ShoppingModel
    {
        public string UserName { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
