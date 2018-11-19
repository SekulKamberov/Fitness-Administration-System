namespace FAS.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserModel 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserPhoto { get; set; }

        public int MembershipType { get; set; }

        public int ProofGiven { get; set; }

        [ValidPhone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Is Payed")]
        public bool IsPayed { get; set; }

        [Display(Name = "Is StarMember")]
        public bool IsStarMember { get; set; }

        [Display(Name = "Is Blocked")]
        public bool IsBlocked { get; set; }
    }
}
