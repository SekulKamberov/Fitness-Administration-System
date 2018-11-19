namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;
    using FAS.Common.Validation.Attributes;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ValidEmail]
        public string Email { get; set; }

    }
}
