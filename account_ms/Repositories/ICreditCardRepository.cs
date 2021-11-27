using System.Collections.Generic;
using System.Threading.Tasks;
using account_ms.Models;

namespace account_ms.Repositories
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> Get(int id);
        Task<IEnumerable<CreditCard>> GetAll();
        Task Add(CreditCard creditCard);
        Task Delete(int id);
        Task Update(CreditCard creditCard);
    }
}
