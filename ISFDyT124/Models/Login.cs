using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISFDyT124.Models
{
    public class Login
    {
        [Key]
        public int LoId { get; set; }

        public int LoUser { get; set; }

        [ForeignKey("LoUser")]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
