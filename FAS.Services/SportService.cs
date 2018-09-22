namespace FAS.Services
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using FAS.Data;
    using FAS.Services.Contracts;
    using FAS.Services.Models;
    using System.Threading.Tasks;
    using FAS.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class SportService : ISportService
    {
        private readonly FASDbContext db;

        public SportService(FASDbContext db)
        {
            this.db = db;
        }
        public async Task<TModel> ByIdAsync<TModel>(int id) where TModel : class => await this.db
             .Sports
             .Where(c => c.Id == id)
             .ProjectTo<TModel>()
             .FirstOrDefaultAsync();

        public async Task<IEnumerable<SportListingServiceModel>> AllActiveAsync() => await this.db
            .Sports
            .Where(c => c.StartDate >= DateTime.UtcNow.Date)
            .OrderByDescending(c => c.StartDate)
            .ThenBy(c => c.Name)
            .ProjectTo<SportListingServiceModel>()
            .ToListAsync();

        public async Task Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var trainerExists = this.db.Users.Any(u => u.Id == trainerId);
            if (!trainerExists)
            {
                return;
            }

            var sport = new Sport
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            await this.db.Sports.AddAsync(sport);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> IsUserSignedInSportAsync(int sportId, string userId) => await this.db
            .Sports.AnyAsync(c => c.Id == sportId && c.Clients.Any(s => s.UserId == userId));

        public async Task<bool> SignUpUserAsync(int sportId, string userId)
        {
            var courseInfo = await GetSportInfo(sportId, userId);

            if (courseInfo == null ||
                courseInfo.StartDate < DateTime.UtcNow.Date ||
                courseInfo.IsStudentEnrolledInSport)
            {
                return false; 
            }

            var studentInCourse = new UserSport
            {
                SportId = sportId,
                UserId = userId
            };

            await this.db.AddAsync(studentInCourse);
            await this.db.SaveChangesAsync();

            return true;          
        }

        public async Task<bool> SignOutUserAsync(int courseId, string studentId)
        {
            var courseInfo = await GetSportInfo(courseId, studentId);

            if (courseInfo == null ||
                courseInfo.StartDate < DateTime.UtcNow.Date ||
                !courseInfo.IsStudentEnrolledInSport)
            {
                return false;
            }
			
            var studentInCourse = await this.db.FindAsync<UserSport>(studentId, courseId);
            this.db.Remove(studentInCourse);

            await this.db.SaveChangesAsync();

            return true;
        }

        private async Task<SportInfoServiceModel> GetSportInfo(int sportId, string clientId) => await this.db
            .Sports
            .Where(c => c.Id == sportId)
            .Select(c => new SportInfoServiceModel
            {
             StartDate = c.StartDate,
             IsStudentEnrolledInSport = c.Clients.Any(s => s.UserId == clientId)
            })
            .FirstOrDefaultAsync();

        public int SportsCount() => this.db.Sports.Count();
    }
}
