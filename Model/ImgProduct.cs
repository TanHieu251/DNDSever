namespace DNDServer.Model
{
    public class ImgProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImgURL { get; set; }
        public Product Product { get; set; }
    }
}
