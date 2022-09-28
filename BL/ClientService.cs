using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _baseRepository;

        public ClientService(IClientRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> UpdateById(Guid id, Client good)
        {
            var response = await _baseRepository.UpdateById(id, good);

            return response;
        }

        public Task<Client> Create(Client good)
        {
            throw new NotImplementedException();
        }
    }

}
