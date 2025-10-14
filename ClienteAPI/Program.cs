using ClienteAPI.Abstractions;
using ClienteAPI.Services;
using Clientes.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ClienteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Contexto>(options =>
                options.UseNpgsql(connectionString));

           
            builder.Services.AddScoped<IClientesService, ClientesService>();

           
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cliente API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // -----------------------------
            // MIDDLEWARE
            // -----------------------------
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente API v1");
                c.RoutePrefix = string.Empty; // Swagger en la raíz /
            });

            // Render maneja HTTPS externamente
            // app.UseHttpsRedirection();

            app.UseAuthorization();

            // Endpoint raíz de prueba
            app.MapGet("/", () => "API ClienteAPI funcionando en Render!");

            // Mapear controladores
            app.MapControllers();

            app.Run();
        }
    }
}
