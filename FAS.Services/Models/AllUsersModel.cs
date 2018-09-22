namespace FAS.Services.Models
{
    using AutoMapper;
    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class AllUsersModel : UserModel, IMapFrom<User>
    {
        public string Id { get; set; }

        //public int ProofGiven { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}
