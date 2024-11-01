using DNDServer.Authen.Request;

namespace DNDServer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        public ApplicationUser User { get; set; }
        public ICollection<OrderDetails> OrderDetail { get; set; }
    }
}
