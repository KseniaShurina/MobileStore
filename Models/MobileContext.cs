using Microsoft.EntityFrameworkCore;

namespace MobileStore.Models
{
    /// <summary>
    /// This class connects the application to the database PostgreSQL.
    /// </summary>
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options) : base(options)
        {
        }
    }
}
