using DemoApp.Contracts;
using DemoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private RepositoryContext Context { get; }

        protected RepositoryBase(RepositoryContext context)
        {
            Context = context;
        }

        public Task<T> CreateAsync(T entity)
        {
            Context.Set<T>().AddAsync(entity);
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task<List<T>> GetAllAsync()
        {
            return Context.Set<T>().ToListAsync();
        }

        public Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>()
                .Where(predicate).ToListAsync();
        }
        public Task<T> FindByIdAsync(Guid id)
        {
            return Context.Set<T>()
             .FindAsync(id).AsTask();
        }
        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsQueryable();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>()
                .Where(predicate);
        }

      
    }
}
