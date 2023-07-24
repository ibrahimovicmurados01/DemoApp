using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Entities;
using DemoApp.Repository.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _context;

        private IRepositoryBase<User> _user;
        private IRepositoryBase<Contact> _contact;

        public RepositoryWrapper(RepositoryContext context)
        {
            _context = context;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        // Singletons
        public IRepositoryBase<User> User
            => _user ??=
                new CreatedModifiedDatesFeatureDecorator<User>(
                    new UniqueIdentifierFeatureDecorator<User>(new UserRepository(_context)));

        // Singleton
        public IRepositoryBase<Contact> Contact
            => _contact ??=
                new TombstoneFeatureDecorator<Contact>(
                    new CreatedModifiedDatesFeatureDecorator<Contact>(
                       new UniqueIdentifierFeatureDecorator<Contact>(
                            new ContactRepository(_context))));
      
    }
}
