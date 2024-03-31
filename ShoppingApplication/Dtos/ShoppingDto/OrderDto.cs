using System;

namespace ShoppingApplication.Dtos.ShoppingDto
{
    public class OrderDto
    {
        public int Id { get; set; }
      
        public string DiscountPromoCode { get; set; }

       public DateTime CloseDate { get; set; }
        public decimal ItemPrice { get; set; }

        public string CurrencyCode { get; set; }
        public int itemId { get; set; }
        public int Quantity { get; set; }
    }
}
