using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> DeleteById(Guid id);
        Task<Client> UpdateById(Guid id, Client good);
        Task<Client> Create(Client good);
    }
}
