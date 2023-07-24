using DemoApp.Contracts;
using DemoApp.Entities.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository.Decorators
{
    internal class UniqueIdentifierFeatureDecorator<T> : FeatureDecorator<T> where T : UniqueIdentifier
    {
        public UniqueIdentifierFeatureDecorator(IRepositoryBase<T> repository) : base(repository)
        {
        }

        public override Task<T> CreateAsync(T entity)
        {
            if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
            return base.CreateAsync(entity);
        }

        public IQueryable<T> GetAll(string userId)
        {
            return base.GetAll();
        }
    }
}
