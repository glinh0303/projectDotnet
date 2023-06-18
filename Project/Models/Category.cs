using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Name { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}
