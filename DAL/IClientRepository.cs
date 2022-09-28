using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> DeleteById(Guid id);
        Task<Client> UpdateById(Guid id, Client client);
        Task<Client> Create(Client client);
    }
}
