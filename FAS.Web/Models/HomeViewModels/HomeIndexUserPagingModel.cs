namespace FAS.Web.Models.HomeViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using FAS.Data.Models;
    using FAS.Services.Models;

    public class HomeIndexUserPagingModel
    {
        public IEnumerable<UserDetailsModel> Users { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public string Search { get; set; }

    }
}
