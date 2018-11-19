namespace FAS.Web.Models.Sports
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using FAS.Services.Models;
    using FAS.Data.Models;

    public class SportDetailsViewModel
    {
        public SportDetailsServiceModel Sport { get; set; }

        public IEnumerable<User> TraiUsersnings { get; set; } = new List<User>();

        public bool IsUserEnrolledInSport { get; set; }
    }
}
