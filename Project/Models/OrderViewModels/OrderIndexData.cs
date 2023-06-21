namespace Project.Models.OrderViewModels
{
    public class OrderIndexData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
     /*   public User User { get; set; }*/
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
