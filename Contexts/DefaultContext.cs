using Microsoft.EntityFrameworkCore;
using MobileStore.Models;

namespace MobileStore.Contexts
{
    /// <summary>
    /// This class connects the application to the database PostgreSQL.
    /// </summary>
    public class DefaultContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public  DbSet<User> Users { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}
