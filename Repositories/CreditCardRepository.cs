using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using account_ms.Data;
using account_ms.Models;

namespace account_ms.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly IDataContext _context;
        public CreditCardRepository(IDataContext context)
        {
            _context = context;
        }


        public async Task Add(CreditCard creditCard)
        {
            _context.CreditCards.Add(creditCard);
            await _context.SaveChangesAsync();
        }


        public async  Task Delete(int id)
        {
            var cardDel = await _context.CreditCards.FindAsync(id);
            if (cardDel == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.CreditCards.Remove(cardDel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CreditCard> Get(int id)
        {
            return await _context.CreditCards.FindAsync(id);
        }

        public async Task<IEnumerable<CreditCard>> GetAll()
        {
            return await _context.CreditCards.ToListAsync();
        }

        public async Task Update(CreditCard creditCard)
        {
            var cardUp = await _context.CreditCards.FindAsync(creditCard.idCard);
            if(cardUp == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                cardUp.dueDate = creditCard.dueDate;
                //cardUp.cardNumber = creditCard.cardNumber;
                //cardUp.cvv = creditCard.cvv;
                //cardUp.idClient = creditCard.idClient;
                await _context.SaveChangesAsync();
            }
        }
    }
}
