namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FAS.Data;
    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class AllSportsViewModel : IMapFrom<Sport>
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<UserSport> Clients { get; set; } = new List<UserSport>();

        public ICollection<User> Trainers { get; set; } = new List<User>();
    }
}
