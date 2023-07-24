using DemoApp.Entities.Models;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository
{
    public class ContactRepository : RepositoryBase<Contact>
    {
        public ContactRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
