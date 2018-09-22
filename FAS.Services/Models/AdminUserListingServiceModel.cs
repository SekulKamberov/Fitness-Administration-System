namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}
