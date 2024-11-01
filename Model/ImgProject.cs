namespace DNDServer.Model
{
    public class ImgProject
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ImgURL { get; set; }
        public Project Projects { get; set; }

    }
}
