namespace DNDServer.Model
{
    public class TypeProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } = 0;
        public string StatusName { get; set; } = "Đang soạn thảo";
        public ICollection<Project> Project { get; set; }
    }
}
