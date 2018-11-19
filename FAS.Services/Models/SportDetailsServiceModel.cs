namespace FAS.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using FAS.Data.Models;
    using FAS.Services.Contracts.Mapping;
    using FAS.Common.Validation.Attributes;

    public class SportDetailsServiceModel : IMapFrom<Sport>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Description(ErrorMessage = "Description must be less 2000 symbols")]
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
                .ForMember(c => c.Clients, cfg => cfg.MapFrom(c => c.UserSports.Count))
                .ForMember(c => c.UserPhoto, cfg => cfg.MapFrom(c => c.Trainer.UserPhoto));
        }
    }
}
