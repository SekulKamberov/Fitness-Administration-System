namespace FAS.Services.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using AutoMapper;

    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class SportListingServiceModel : IMapFrom<Sport>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Trainer { get; set; }

        public string UserPhoto { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Sport, SportListingServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => $"{c.Trainer.FirstName} {c.Trainer.LastName}"))
                .ForMember(c => c.UserPhoto, cfg => cfg.MapFrom(c => c.Trainer.UserPhoto));
        }
    }
}
