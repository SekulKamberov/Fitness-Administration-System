namespace FAS.Web.Models.Users
{
    using FAS.Services.Models;
    using System.Collections.Generic;

    public class UserPageListModel 
    {
        public IEnumerable<UserDetailsModel> Users { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
