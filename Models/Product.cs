
#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace CoreServices.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
