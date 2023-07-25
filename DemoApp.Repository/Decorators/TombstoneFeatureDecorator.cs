using DemoApp.Contracts;
using DemoApp.Entities.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository.Decorators
{
    internal class TombstoneFeatureDecorator<T> : FeatureDecorator<T> where T : class, TombstoneFeature
    {
        public TombstoneFeatureDecorator(IRepositoryBase<T> repository) : base(repository)
        {
        }
        public override Task<T> CreateAsync(T entity)
        {
            entity.Tombstoned = false;
            return base.CreateAsync(entity);
        }
        public override Task DeleteAsync(T entity)
        {
            entity.Tombstoned = true;
            return base.UpdateAsync(entity);
        }

        public override Task<List<T>> GetAllAsync()
        {
            return base.FindByAsync(r => r.Tombstoned == false);
        }

        public override IQueryable<T> GetAll()
        {
            return base.GetAll().Where(r => r.Tombstoned == false);
        }
    
    }
}
