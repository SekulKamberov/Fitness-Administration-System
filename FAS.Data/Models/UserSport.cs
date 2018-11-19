namespace FAS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserSport
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }
    }
}
