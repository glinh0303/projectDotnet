using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class Member
    {
        [Key]
        public int UserId { get; set; }
        public ICollection<AppUser> User { get; set; }
        public int RankId { get; set; }
        public Rank rank { get; set; }

        public double totalPayment { get; set; }
    }
}
