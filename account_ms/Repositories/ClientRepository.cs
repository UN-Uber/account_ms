using System.Collections.Generic;
using System.Threading.Tasks;
using account_ms.Models;
using account_ms.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace account_ms.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDataContext _context;
        public ClientRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task Add(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var clientDel = await _context.Clients.FindAsync(id);
            if (clientDel == null){
                throw new NullReferenceException();
            }else{
                _context.Clients.Remove(clientDel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Client> Get(int id)
        {
            return await _context.Clients.FindAsync(id);

        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task Update(Client client)
        {
            var clientUp = await _context.Clients.FindAsync(client.idClient);
            if (clientUp == null){
                throw new NullReferenceException();
            }else{
                clientUp.active = client.active;
                clientUp.email = client.email;
                clientUp.fName =  client.fName;
                clientUp.password = client.password;
                clientUp.sName = client.sureName;
                clientUp.telNumber = client.telNumber;
                clientUp.sureName = client.sureName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CreditCard>> GetCards(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                throw new NullReferenceException();
            }
            else {
                var cards = await _context.CreditCards.Where(x => x.idClient == client.idClient).ToListAsync();
                return cards;
            }
        }

    }
}