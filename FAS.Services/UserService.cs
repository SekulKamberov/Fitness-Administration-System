namespace FAS.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper.QueryableExtensions;

    using FAS.Data;
    using FAS.Services.Models;
    using FAS.Services.Contracts;
    using FAS.Data.Models;
    using System;

    public class UserService : IUserService
    {
        private readonly FASDbContext db;

        public UserService(FASDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel UserById(string id) => this.db.Users
            .Where(p => p.Id == id).ProjectTo<UserDetailsModel>().FirstOrDefault();

        public IEnumerable<UserDetailsModel> AllWithBlocked() => this.db.Users.ProjectTo<UserDetailsModel>();

        public IEnumerable<UserDetailsModel> All(int page, int pageSize) => this.db.Users
            .Where(p => p.IsBlocked == false).OrderByDescending(p => p.FirstName)
            .Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<UserDetailsModel>().ToList();

        public int UserCount() => this.db.Users.Count();

        public void Edit(UserDetailsModel member)
        {
            var user = this.db.Users.Find(member.Id);
            if (member == null)
            {
                return;
            }
            user.FirstName = member.FirstName;
            user.LastName = member.LastName;
            user.UserPhoto = member.UserPhoto;
            user.Email = member.Email;
            user.MembershipType = member.MembershipType;
            user.ProofGiven = member.ProofGiven;
            user.Age = member.Age;
            user.Sex = member.Sex;
            user.Height = member.Height;
            user.Weight = member.Weight;
            user.PhoneNumber = member.PhoneNumber;
            user.Date = member.Date;
            user.Adress = member.Adress;
            user.IsBlocked = member.IsBlocked;
            user.IsPayed = member.IsPayed;
            user.IsTrainer = member.IsTrainer;
            user.IsStarMember = member.IsStarMember;

            this.db.SaveChanges();
        }
 
        public IEnumerable<UserDetailsModel> SortByGander(int id, int page = 1, int pageSize = 10)
        {
            return this.db.Users
                .Where(p => p.Sex == (Sex)id && p.IsBlocked == false)
                .OrderBy(p => p.IsStarMember)
                .ThenBy(p => p.Age)
                .ThenBy(p => p.IsPayed)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserDetailsModel>().ToList();
        }

        public void NewSport(SportDetailsModel sport)
        {
            var sportModel = new Sport()
            {
                Name = sport.Name,
                TrainerId = sport.TrainerId,
                StartDate = sport.StartDate,
                EndDate = sport.EndDate
            };
            this.db.Sports.Add(sportModel);
            this.db.SaveChanges();
        }

        //TO DO!  implement reasons for blocking and sorting by level of guilt and punishment
        public IEnumerable<UserDetailsModel> BlockedUsers(int page, int pageSize)
        {
            return this.db.Users
                .Where(p => p.IsBlocked == true)
                .OrderBy(p => p.IsStarMember)
                .ThenBy(p => p.Age)
                .ThenBy(p => p.IsPayed)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserDetailsModel>().ToList();
        }

        public IEnumerable<UserDetailsModel> NotPaid(int page, int pageSize)
        {
            return this.QueryarableUsers().Where(p => p.IsPayed == false)
                        .OrderBy(c => c.ProofGiven)
                        .ThenBy(c => c.Date)
                        .ThenBy(c => c.FirstName)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ProjectTo<UserDetailsModel>().ToList();
        }

        public SportDetailsModel GetTrainers()
        {
            SportDetailsModel model = new SportDetailsModel()
            {
                Trainers = this.db.Users
                .Where(c => c.IsTrainer == true)
                .Select(s => new TrainersModel() { TrainerId = s.Id, FirstName = s.FirstName, LastName = s.LastName })
            };
            return model;
        }

        public bool Exists(string id) => this.db.Users.Any(c => c.Id == id);

        public IQueryable<User> QueryarableUsers()
        {
            return this.db.Users.AsQueryable<User>();
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            return await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
        }
    }
}
