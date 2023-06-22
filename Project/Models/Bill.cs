using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Double Payment { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        //[Required]
        //[StringLength(maximumLength: 100, MinimumLength = 15)]
        public string Address { get; set; }
        //[Required]
        public string Phone { get; set; }
        public string Note { get; set; }
        [ForeignKey(nameof(OrderDetail))]
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    
        public OrderStatus Status { get; set; }

    }
}

