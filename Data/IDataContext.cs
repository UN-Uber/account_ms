using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using account_ms.Models;

namespace account_ms.Data
{
    public interface IDataContext
    {
        DbSet<Client> Clients { get; set;  }
        DbSet<CreditCard> CreditCards {get; set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
