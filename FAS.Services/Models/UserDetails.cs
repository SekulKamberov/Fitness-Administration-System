namespace FAS.Services.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserDetails
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        public int Age { get; set; }

        public int Sex { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public DateTime Date { get; set; }

        public string Adress { get; set; }

        public string UserPhoto { get; set; }

        public int MembershipType { get; set; }

        public int ProofGiven { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPayed { get; set; }

        public bool IsStarMember { get; set; }

        public bool IsBlocked { get; set; }
    }
}
