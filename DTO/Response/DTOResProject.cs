using DNDServer.Model;

namespace DNDServer.DTO.Response
{
    public class DTOResProject
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Feature { get; set; }
        public int Status { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public string StatusName { get; set; }
        public int TypeData { get; set; }
        public List<ImgProject> ListImages { get; set; }


        public DTOResProject()
        {
            ListImages = new List<ImgProject>();
        }
    }
}
