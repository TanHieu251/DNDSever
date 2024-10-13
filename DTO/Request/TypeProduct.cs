namespace DNDServer.DTO.Request
{
    public class TypeProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public string? StatusName { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<Product>? Product { get; set; }
    }
}
