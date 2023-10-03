using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using NIJ.Web.Models.Infra;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NIJ.Web.Data
{
    public class IESContext : IdentityDbContext<UserAplication>
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActivityUser>()
                .HasKey(au => new { au.ActivityId, au.UserId });
            
            modelBuilder.Entity<ActivityUser>()
                .HasOne(a => a.Activity)
                .WithMany(au => au.ActivitiesUsers)
                .HasForeignKey(a => a.ActivityId);

            modelBuilder.Entity<ActivityUser>()
                .HasOne(a => a.User)
                .WithMany(au => au.ActivitiesUsers)
                .HasForeignKey(a => a.UserId);

        }
    }
}
