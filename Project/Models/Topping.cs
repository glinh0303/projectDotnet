using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Topping
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1)]

        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
    }
}
