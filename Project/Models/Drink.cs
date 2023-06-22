using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public enum DrinkSize
    {
        S, M, L
    }
    public class Drink
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
        [Range(0,int.MaxValue)]

        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        [ForeignKey(nameof(Category))]
       /* [Display(Name = "Category")]*/
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
