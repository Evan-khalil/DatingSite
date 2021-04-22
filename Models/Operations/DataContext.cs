
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Diagnostics;

namespace DatingSite
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Requests> Requests { get; set; }
    }
}
