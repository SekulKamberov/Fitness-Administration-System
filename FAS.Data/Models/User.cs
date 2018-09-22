namespace FAS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;    

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }

        public int ProofGiven { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public Sex Sex { get; set; }

        public string UserPhoto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [RegularExpression("[A-Za-z0-9].*")]
        public string Adress { get; set; }

        [Range(60, 260)]
        public int Height { get; set; }

        [Range(35, 170)]
        public int Weight { get; set; }

        public MembershipType MembershipType { get; set; }

        public bool IsPayed { get; set; }

        public bool IsStarMember { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsTrainer { get; set; }

        public ICollection<UserSport> Sports { get; set; } = new List<UserSport>();

        public ICollection<Sport> Trainings { get; set; } = new List<Sport>();

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
