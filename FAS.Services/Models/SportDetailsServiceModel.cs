namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;

    public class SportDetailsServiceModel : IMapFrom<Sport>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Trainer { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Clients { get; set; }

        public string UserPhoto { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Sport, SportDetailsServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => $"{c.Trainer.FirstName} {c.Trainer.LastName}"))
                .ForMember(c => c.Clients, cfg => cfg.MapFrom(c => c.Clients.Count))
                .ForMember(c => c.UserPhoto, cfg => cfg.MapFrom(c => c.Trainer.UserPhoto));
        }
    }
}
