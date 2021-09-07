namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }//to know which productid belongs to the shoppingCartItem
        public string ProductName { get; set; }//instead of getting product name from catalog Microservice duplicate here for performance
    }
}