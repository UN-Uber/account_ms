using System.Threading.Tasks;
using account_ms.Models;
using System.Collections.Generic;
using System.Linq;

namespace account_ms.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Get(int id);
        Task<IEnumerable<Client>> GetAll();
        Task Add(Client client);
        Task Delete(int id);
        Task Update(Client client);
        Task<IEnumerable<CreditCard>> GetCards(int id);
        Task<Client> getEmail(string email);
        Task<Client> getTelNumber(long telNumber);
    }
}