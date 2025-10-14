using ClienteAPI.Abstractions;
using ClienteAPI.Services;
using ClienteAPI.Services.DI;
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

            // Conexión a SQL Server
            builder.Services.AddDbContext<Contexto>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));

            // Servicios
            builder.Services.AddScoped<IClientesService, ClientesService>();

            // Controladores y Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Cliente API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();


        }
    }
}
