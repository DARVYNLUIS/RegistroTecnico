using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Components;
using RegistroTecnico.DAL;
using RegistroTecnico.Services;


namespace RegistroTecnico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //Obtenemos el ConStr para usarlo en el contexto
            var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
          

            //Agregamos el contexto al builder con el ConStr
            builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));


            //Inyeccion del service
            builder.Services.AddScoped<TecnicoService>();
            builder.Services.AddScoped<ClienteService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
