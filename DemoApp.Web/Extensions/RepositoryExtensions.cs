using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Repository;

namespace DemoApp.Web.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(
      this IServiceCollection services)
      => services
          .AddScoped<IRepositoryWrapper, RepositoryWrapper>()
          .AddScoped<IRepositoryBase<User>, UserRepository>()
          .AddScoped<IRepositoryBase<Contact>, ContactRepository>();
    }
}
