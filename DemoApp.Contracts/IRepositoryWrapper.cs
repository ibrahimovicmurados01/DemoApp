using DemoApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Contracts
{
    // IRepositoryWrapper is an interface that acts as a contract for a repository pattern.
    public interface IRepositoryWrapper
    {
        // IRepositoryBase<User> provides access to user-related data operations.
        IRepositoryBase<User> User { get; }

        // IRepositoryBase<Contact> provides access to contact-related data operations.
        IRepositoryBase<Contact> Contact { get; }

        // Task SaveAsync() saves changes made through repository operations to the data store.
        // It acts as a Unit of Work and should be used to commit changes as a single transaction.
        Task SaveAsync();
    }

}
