using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> contextOptions)
           : base(contextOptions)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
