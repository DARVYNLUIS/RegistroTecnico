using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Clientes.Data.DAL;

namespace Clientes.Data.DI
{
    public static class DbContextRegistrar
    {
        public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services)
        {
            services.AddDbContextFactory<Contexto>(options =>
                options.UseSqlServer("Name=SqlConStr"));

            return services;
        }
    }
}
