namespace DNDServer.Model
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string FullName { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
