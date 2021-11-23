using AppComensal.Client.Servicios;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Logging.SetMinimumLevel(LogLevel.Debug);

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<PedidoService>();
            builder.Services.AddSingleton<ConsumibleHolder>();
            builder.Services.AddSingleton<RestauranteService>();
            builder.Services.AddSingleton<OrdenService>();

            var host = builder.Build();

            var localStorageSvc = host.Services.GetRequiredService<ILocalStorageService>();
            var restauranteService = host.Services.GetRequiredService<RestauranteService>();
            await restauranteService.InitializeAsync(localStorageSvc);

            await host.RunAsync();
        }
    }
}
