namespace DNDServer.DTO.Request
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
