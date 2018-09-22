namespace FAS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FAS.Data.Models;

    public class UserSport
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public Grade? Grade { get; set; }
    }
}
