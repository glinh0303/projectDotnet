namespace Project.Models
{
    public class Size
    {
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
        public DrinkSize DrinkSize { get; set; }
    }
}
