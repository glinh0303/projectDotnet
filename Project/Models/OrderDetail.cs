using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Project.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }      
        public int Quantity { get; set; }
        public double Payment { get; set; }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        public DrinkSize Size { get; set; }
        public ICollection<Topping> Toppings { get; set; }
      /*  public Profile Profile { get; set; }*/
        public int UserId { get; set; }
    }
}
