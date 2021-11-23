using AppComensal.Client.Infraestructura;
using AppComensal.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppComensal.Client.Servicios
{
    public class RestauranteService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navManager;
        private readonly ILogger<RestauranteService> logger;

        private RestauranteModel restaurante;
        private MenuModel[] menus;
        private ILocalStorageService LocalStore;

        public bool TodoListo = false;

        public RestauranteService(HttpClient Http, NavigationManager navigationManager, ILogger<RestauranteService> logger)
        {
            this.http = Http;
            this.navManager = navigationManager;
            this.logger = logger;
        }

        public async Task InitializeAsync(ILocalStorageService localStore) {
            this.LocalStore = localStore;

            int? RestauranteId = null;
            int? MesaId = null;
            bool guardarDatos = false;

            var queryString = navManager.QueryString();

            var restId = queryString["restauranteId"];
            var mesaId = queryString["mesaId"];

            if (int.TryParse(restId, out var res))
                RestauranteId = res;

            if (int.TryParse(mesaId, out var mes))
                MesaId = mes;

            if (!RestauranteId.HasValue || !MesaId.HasValue)
            {
                RestauranteId = await ObtenerRestauranteId();
                MesaId = await ObtenerMesa();
            }
            else
                guardarDatos = true;

            try
            {
                if (RestauranteId.HasValue && MesaId.HasValue)
                {
                    if (restaurante is null)
                        restaurante = await http.GetFromJsonAsync<RestauranteModel>($"api/restaurante/ObtenerInformacion/{RestauranteId}");

                    if (menus is null)
                        menus = await http.GetFromJsonAsync<MenuModel[]>($"api/restaurante/obtenermenu/{RestauranteId}");

                    if(guardarDatos)
                        await GuardarDatosDeSesion(MesaId.Value, RestauranteId.Value);

                    TodoListo = true;
                }
                else
                    this.navManager.NavigateTo("oops");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                this.navManager.NavigateTo("error");
            }
        }

        public RestauranteModel ObtenerInfoRestaurante() => restaurante;

        public MenuModel[] ObtenerMenus() => menus;

        public async Task GuardarDatosDeSesion(int mesaId, int restauranteId) {
            await this.LocalStore.SetItemAsStringAsync("restauranteId", restauranteId.ToString());
            await this.LocalStore.SetItemAsStringAsync("mesaId", mesaId.ToString());
        }

        public async Task<int> ObtenerMesa() => int.Parse(await this.LocalStore.GetItemAsStringAsync("mesaId"));

        public async Task<int> ObtenerRestauranteId() => int.Parse(await this.LocalStore.GetItemAsStringAsync("restauranteId"));

        public async Task LlamarMesero() {
            var msn = new MensajeModel { MesaId = await ObtenerMesa() };
            await http.PostAsJsonAsync("api/restaurante/LlamarMesero", msn);
        }

        public async Task PedirCuenta() {
            var msn = new MensajeModel { MesaId = await ObtenerMesa() };
            await http.PostAsJsonAsync("api/restaurante/PedirCuenta", msn);
        }
    }
}
