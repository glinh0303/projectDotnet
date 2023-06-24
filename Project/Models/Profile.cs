using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public enum Gender
    {
        Male, Female, Other
    }

    public enum Nationality
    {
        VietNamese, ThaiLand, England, American, Australian
    }
    public class Profile
    {
        [Key]
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public string Avatar { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 1)]

        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 20)]
        public string Address { get; set; }
        [RegularExpression("[0-9]{10}")]
        public string Phone { get; set; }
        public Nationality Nationality { get; set; }
        public int RankId { get; set; }
        public decimal totalPayment { get; set; }
    }
}

