using DemoApp.Contracts;
using DemoApp.Entities.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Repository.Decorators
{
    internal class CreatedModifiedDatesFeatureDecorator<T> : FeatureDecorator<T> where T : class, CreatedModifiedFeature
    {
        public CreatedModifiedDatesFeatureDecorator(IRepositoryBase<T> repository) : base(repository)
        {
        }

        public override Task<T> CreateAsync(T entity)
        {
            var dateTime = DateTime.UtcNow;
            entity.Created = dateTime;
            entity.Modified = dateTime;
            return base.CreateAsync(entity);
        }

        public override Task<T> UpdateAsync(T entity)
        {
            entity.Modified = DateTime.UtcNow;
            return base.UpdateAsync(entity);
        }
    }
}
