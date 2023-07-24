using DemoApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository.Decorators
{
    abstract class FeatureDecorator<T> : IRepositoryBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repository;

        public FeatureDecorator(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public virtual Task<T> CreateAsync(T entity)
           => _repository.CreateAsync(entity);

        public virtual Task<T> UpdateAsync(T entity)
            => _repository.UpdateAsync(entity);

        public virtual Task DeleteAsync(T entity)
            => _repository.DeleteAsync(entity);

        public virtual Task<List<T>> GetAllAsync()
            => _repository.GetAllAsync();

        public virtual Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
            => _repository.FindByAsync(predicate);
        public virtual Task<T> FindByIdAsync(Guid id)
         => _repository.FindByIdAsync(id);
        public virtual IQueryable<T> GetAll()
            => _repository.GetAll();

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
            => _repository.FindBy(predicate);
    }
}
