namespace FAS.Data.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class Sport
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public string TrainerId { get; set; }

        public User Trainer { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<UserSport> UserSports { get; set; } = new List<UserSport>();

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
