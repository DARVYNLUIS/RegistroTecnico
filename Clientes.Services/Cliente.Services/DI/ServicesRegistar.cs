using ClienteAPI.Abstractions;
using Clientes.Data.DAL;
using Clientes.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace ClienteAPI.Services.DI
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConStr");

            services.AddDbContextFactory<Contexto>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IClientesService, ClientesService>();

            return services;
        }

    }
}
