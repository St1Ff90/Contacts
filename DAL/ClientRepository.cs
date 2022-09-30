using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyAppContext _dbContext;

        public ClientRepository(MyAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Create(Client client)
        {
            client.Id = Guid.NewGuid();
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<Client> DeleteById(Guid id)
        {
            Client client = await _dbContext.Clients.Where(x => x.Id == id).Include(x => x.Contacts).Include(x => x.Adresses).FirstAsync();
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _dbContext.Clients.Include(x => x.Contacts).Include(x => x.Adresses).ToListAsync();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _dbContext.Clients.Where(x => x.Id == id).Include(x => x.Contacts).Include(x => x.Adresses).FirstAsync();
        }

        public async Task<Client> UpdateById(Guid id, Client client)
        {
            client.Id = id;
            _dbContext.Entry(client).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return client;
        }
    }
}