using DemoApp.Entities.Models;
using DemoApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DemoApp.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public User FindByUsername(string username)
        {
            return base.FindBy(x=>x.Username==username).FirstOrDefault();
        }
    }
}
