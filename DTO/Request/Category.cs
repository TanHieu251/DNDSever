namespace DNDServer.DTO.Request
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<TypeProduct> TypeProducts { get; set; }
    }
}
