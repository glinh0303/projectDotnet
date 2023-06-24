using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Project.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }      
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 3)")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Payment { get; set; }
        public Drink Drink { get; set; }
        public DrinkSize Size { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }
        public ICollection<Topping> Toppings { get; set; }
    }
}
