namespace DNDServer.DTO.Request
{
    public class Project
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ThumbNail { get; set; }
        public string? Feature { get; set; }
        public int Status { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly? DateEnd { get; set; }
        public string? StatusName { get; set; }
        public int TypeData { get; set; }
        public TypeProject? TypeProject { get; set; }
        public ICollection<ImgProject>? ImgProjects { get; set; }

    }
}
