namespace FAS.Services.Models
{
    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class TrainersModel : IMapFrom<User>
    {
        public string TrainerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
