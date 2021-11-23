using AppAdministracion.Infraestructura.Identity;
using AppAdministracion.Infraestructura.Mensajeria;
using Datos.Models;
using Entidades.Interfaces;
using Identity.Model;
using Identity.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositorio;
using System.Globalization;

namespace AppAdministracion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InfoOfRestaurantContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RestauranteConection"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RestauranteConection"]));

            services
                .AddIdentity<Usuario, Rol>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddSignalR();

            services.AddScoped<IRepositorioRestaurante, RepositorioRestaurante>();
            services.AddScoped<IRepositorioFuncionario, RepositorioFuncionario>();
            services.AddScoped<IRepositorioMenu, RepositorioMenu>();
            services.AddScoped<IRepositorioConsumible, RepositorioConsumible>();
            services.AddScoped<IRepositorioMesa, RepositorioMesa>();
            services.AddScoped<IUserClaimsPrincipalFactory<Usuario>, AdditionalUserClaimsPrincipalFactory>();
            services.AddHostedService<RedisBroker>();

            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo("es-CO");
            cultureInfo.NumberFormat.CurrencySymbol = "$";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapHub<FuncionarioHub>("/notificaciones");
            });

            IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
        }
    }
}
