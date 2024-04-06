using CargaHorariaCRUD.Repositories;
using CargaHorariaCRUD.Repositories.Interfaces;
using CargaHorariaCRUD.WebHelper;
using CargaHorariaCRUD.WebHelper.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargaHorariaCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = "server=localhost;user=root;password=;database=usuarios_ifms";
            var serverVersion = new MySqlServerVersion(new Version(8,0,36));

            // Replace 'YourDbContext' with the name of your own DbContext derived class.
            builder.Services.AddDbContext<UsuariosIfmsContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString,serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine,LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddSingleton<IAdmRepository, AdmRepository>();
            builder.Services.AddSingleton<IEnvioRepository, EnvioRepository>();
            builder.Services.AddSingleton<ISessao, Sessao>();

            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if(!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /*
            app.UseStatusCodePages(async context =>
            {
                if(context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.HttpContext.Response.ContentType = "text/html";
                    await context.HttpContext.Response.WriteAsync("<html><body>Page not found</body></html>");
                }
                if(context.HttpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    context.HttpContext.Response.ContentType = "text/html";
                    await context.HttpContext.Response.WriteAsync("<html><body>Internal Server error</body></html>");
                }
            });*/

            app.UseStatusCodePagesWithRedirects("/Erro/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=EstudanteLogin}/{id?}");

            app.Run();
        }
    }
}
