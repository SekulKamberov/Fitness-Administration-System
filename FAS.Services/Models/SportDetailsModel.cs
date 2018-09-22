namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SportDetailsModel : IMapFrom<Sport>, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string TrainerId { get; set; }

        public IEnumerable<TrainersModel> Trainers { get; set; } = new List<TrainersModel>();

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fitness Group start")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fitness Group end")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult("Start date cannot be in the past.");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start date should be before end date.");
            }
        }
    }
}
