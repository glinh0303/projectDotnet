using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Double Payment { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 15)]
        public string Address { get; set; }
        [Required]
        public int Phone { get; set; }
        public string Note { get; set; }
        public int OrderStatus { get; set; } = 0;
        [ForeignKey(nameof(OrderDetail))]
        public int OrderDetailId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        /*public User User { get; set; }*/
    }
}
