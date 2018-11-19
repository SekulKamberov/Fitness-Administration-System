namespace FAS.Services.Contracts
{
    using FAS.Data.Models;
    using FAS.Services.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISportService
    {
        //IEnumerable<AllSportsViewModel> All(int page = 1, int pageSize = 10);

        Task<IEnumerable<User>> UsersInSport(int sportId);

        Task Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId);

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<IEnumerable<SportListingServiceModel>> AllActiveAsync();

        Task<bool> IsUserSignedInSportAsync(int sportId, string userId);

        Task<bool> SignUpUserAsync(int sportId, string userId);

        Task<bool> SignOutUserAsync(int sportId, string userId);

        int SportsCount();
    }
}
