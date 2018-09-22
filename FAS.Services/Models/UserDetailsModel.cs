namespace FAS.Services.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class UserDetailsModel : IMapFrom<User>
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

        public Sex Sex { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [RegularExpression("[A-Za-z0-9].*")]
        public string Adress { get; set; }

        public string UserPhoto { get; set; }

        public MembershipType MembershipType { get; set; }

        public int ProofGiven { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool IsPayed { get; set; }

        public bool IsStarMember { get; set; }

        public bool IsTrainer { get; set; }

        public bool IsBlocked { get; set; }
    }
}
