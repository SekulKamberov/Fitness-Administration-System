namespace FAS.Services.Contracts
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FAS.Data.Models;
    using FAS.Services.Models;
    using System;

    public interface IUserService
    {
        

        UserDetailsModel UserById(string id);

        IEnumerable<UserDetailsModel> All(int page, int pageSize);

        IEnumerable<UserDetailsModel> AllWithBlocked();

        IEnumerable<UserDetailsModel> SortByGander(int sex, int page = 1, int pageSize = 10);

        IEnumerable<UserDetailsModel> BlockedUsers(int page, int pageSize);

        void Edit(UserDetailsModel user);

        IEnumerable<UserDetailsModel> NotPaid(int page, int pageSize);

        void NewSport(SportDetailsModel sport);

        int UserCount();

        SportDetailsModel GetTrainers();

        IQueryable<User> QueryarableUsers();

        bool Exists(string id);

        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
