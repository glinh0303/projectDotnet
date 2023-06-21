using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
    /*    public User User { get; set; }*/
        public OrderDetail OrderDetail { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Payment { get; set; }

    }
}
