using Microsoft.EntityFrameworkCore;
using account_ms.Models;

namespace account_ms.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients {get; set;}
        public DbSet<CreditCard> CreditCards {get; set;}
    }
}
