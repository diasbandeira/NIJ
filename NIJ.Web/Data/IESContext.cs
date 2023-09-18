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
    }
}
