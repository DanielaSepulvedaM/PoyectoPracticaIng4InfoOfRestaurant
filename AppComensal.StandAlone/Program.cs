using AppComensal.Client.Servicios;
using AppComensal.StandAlone;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<PedidoService>();
builder.Services.AddSingleton<ConsumibleHolder>();
builder.Services.AddSingleton<RestauranteService>();
builder.Services.AddSingleton<OrdenService>();

var host = builder.Build();

var cultureInfo = new CultureInfo("es-CO");
cultureInfo.NumberFormat.CurrencySymbol = "$";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var localStorageSvc = host.Services.GetRequiredService<ILocalStorageService>();
var restauranteService = host.Services.GetRequiredService<RestauranteService>();
await restauranteService.InitializeAsync(localStorageSvc);

var ordenService = host.Services.GetRequiredService<OrdenService>();
await ordenService.InitializeAync();

await host.RunAsync();
