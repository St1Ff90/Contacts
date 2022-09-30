using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactsMVC.Models
{
    public class ClientViewListModel
    {
        public IEnumerable<Client> Clients { get; set; }

        public SelectList FulName { get; set; }
    }
}
