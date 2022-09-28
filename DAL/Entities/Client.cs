using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FullName { get; set; } 
        public string ShortName { get; set; }
        public int TaxNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Adress> Adresses { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        public Client()
        {
            Adresses = new List<Adress>();
            Contacts = new List<Contact>();
        }
    }
}
