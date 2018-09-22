namespace FAS.Web.Models.AccountViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FAS.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }

        [Required]
        public int ProofGiven { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public Sex Sex { get; set; }
        
        public IFormFile UserPhoto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Adress { get; set; }
  
        public string PhoneNumber { get; set; }

        //in cm
        [Range(60, 260)]
        public int Height { get; set; }

        //in kg
        [Range(35, 170)]
        public int Weight { get; set; }

        public MembershipType MembershipType { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Email == "se4ko@abv.bg" && PhoneNumber == "359899750224")
            {
                yield return new ValidationResult("Sorry, phone number or e-mail are already registered");
            }
            else if (PhoneNumber == "899750224")
            {
                yield return new ValidationResult("Sorry, phone number is already registered");
            }
            else if (Email == "se4ko@abv.bg")
            {
                yield return new ValidationResult("Sorry, e-mail are already registered");
            }
        }
    }
}
