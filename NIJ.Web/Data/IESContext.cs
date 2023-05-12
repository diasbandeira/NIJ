using Microsoft.EntityFrameworkCore;
using NIJ.Web.Models;

namespace NIJ.Web.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {

        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
