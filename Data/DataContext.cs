using Microsoft.EntityFrameworkCore;
using account_ms.Models;
using System.Linq;

namespace account_ms.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Client>()
            .HasIndex(p => new { p.telNumber })
            .IsUnique(true);
        modelBuilder.Entity<Client>()
            .HasIndex(p => new { p.email})
            .IsUnique(true);
        modelBuilder.Entity<CreditCard>()
            .HasIndex(p => new { p.cardNumber })
            .IsUnique(true);
        }

        public DbSet<Client> Clients {get; set;}
        public DbSet<CreditCard> CreditCards {get; set;}
    }
}
