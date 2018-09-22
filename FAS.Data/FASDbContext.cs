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
                .WithMany(s => s.Sports)
                .HasForeignKey(sc => sc.UserId);

            builder
               .Entity<UserSport>()
               .HasOne(sc => sc.Sport)
               .WithMany(c => c.Clients)
               .HasForeignKey(sc => sc.SportId);

            builder
                .Entity<Sport>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(c => c.TrainerId);

            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Articles)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);         
        }
    }
}
