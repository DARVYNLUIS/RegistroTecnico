using ClienteAPI.Abstractions;
using Clientes.Data.DAL;
using Clientes.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace ClienteAPI.Services.DI
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            
            services.AddDbContextFactory<Contexto>(options =>
                options.UseNpgsql(connectionString)
                       .EnableSensitiveDataLogging() 
                       .LogTo(Console.WriteLine, LogLevel.Information));

            
            services.AddScoped<IClientesService, ClientesService>();

            return services;
        }
    }
}
