namespace ShoppingApplication.Dtos.ShoppingDto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }

        public string Description { get; set; }

        public int QTY { get; set; }
        public decimal Price { get; set; }

        public int UnitOfMeasureId { get; set; }

        public string PromoCode { get; set; }
    }
}
