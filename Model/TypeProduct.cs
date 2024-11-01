using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DNDServer.Model
{
    public class TypeProduct
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } = 0;
        public string StatusName { get; set; } = "Đang soạn thảo";

        public ICollection<Product> Product { get; set; }
    }
}
