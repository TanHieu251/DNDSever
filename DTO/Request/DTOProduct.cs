using DNDServer.Model;

namespace DNDServer.DTO.Request
{
    public class DTOProduct
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Feature { get; set; }
        public string Specfication { get; set; }
        public string Review { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int TypeData { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }



 
    }
}
