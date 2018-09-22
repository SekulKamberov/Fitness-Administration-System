namespace FAS.Web.Models.Sports
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using FAS.Services.Models;

    public class SportDetailsViewModel
    {
        public SportDetailsServiceModel Sport { get; set; }

        public bool IsUserEnrolledInSport { get; set; }
    }
}
