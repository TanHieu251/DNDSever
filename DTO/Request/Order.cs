using DNDServer.Authen.Request;

namespace DNDServer.DTO.Request
{
    public class Order
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public int Phone {  get; set; }
        public string? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public string? StatusName { get; set; }
    
       public ApplicationUser? User { get; set; }
        public ICollection<OrderDetails>? OrderDetail {  get; set; }    
    }
}
