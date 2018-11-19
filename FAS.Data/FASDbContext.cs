namespace FAS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using FAS.Data.Models;

    public class FASDbContext : IdentityDbContext<User>
    {
        public DbSet<Sport> Sports { get; set; }

        public FASDbContext(DbContextOptions<FASDbContext> options)
            : base(options)
        {         
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSport>()
           .HasKey(uc => new { uc.UserId, uc.SportId });

            builder
                .Entity<UserSport>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserSports)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<UserSport>()
               .HasOne(sc => sc.Sport)
               .WithMany(c => c.UserSports)
               .HasForeignKey(sc => sc.SportId)
               .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Schedule>()
            //    .HasOne(a => a.Sport)
            //    .WithMany(a => a.Schedules)
            //    .HasForeignKey(a => a.SportId);
                 

            base.OnModelCreating(builder);
        }
    }
}
