namespace FAS.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using FAS.Data;
    using FAS.Services.Models;
    using FAS.Services.Contracts;
    using FAS.Data.Models;
    using AutoMapper.QueryableExtensions;

    public class ClientService : IClientService
    {
        private readonly FASDbContext db;

        public ClientService(FASDbContext db)
        {
            this.db = db;
        }
 
        //public void AddClientProfile(ProfileModel profile)
        //{
        //    var client = this.db.Clients.Add(new Client()
        //    {
        //        Id = profile.Id,
        //        Info = profile.Info,
        //        Sports = profile.

        //    });
        //    this.db.SaveChanges();
        //}
    }
}
