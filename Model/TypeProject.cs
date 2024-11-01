namespace DNDServer.Model
{
    public class TypeProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
