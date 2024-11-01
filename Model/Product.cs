namespace DNDServer.Model
{
    public class Product
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Feature { get; set; }
        public string Specfication { get; set; }
        public string Review { get; set; }
        public string ThumbNail { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int TypeData { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public TypeProduct TypeProduct { get; set; }
        public ICollection<ImgProduct> ImgProjects { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
