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


        public RestauranteService(HttpClient Http, NavigationManager navigationManager, ILogger<RestauranteService> logger)
        {
            this.http = Http;
            this.navManager = navigationManager;
            this.logger = logger;
        }

        public async Task InitializeAsync(ILocalStorageService localStore) {
            this.LocalStore = localStore;
            
            var queryString = navManager.QueryString();

            var restId = queryString["restauranteId"];
            var mesaId = queryString["mesaId"];

            int? RestauranteId = null;
            if (int.TryParse(restId, out var res))
                RestauranteId = res;

            int? MesaId = null;
            if (int.TryParse(mesaId, out var mes))
                MesaId = mes;

            try
            {
                if (RestauranteId.HasValue && MesaId.HasValue)
                {
                    if (restaurante is null)
                        restaurante = await http.GetFromJsonAsync<RestauranteModel>($"api/restaurante/ObtenerInformacion/{RestauranteId}");

                    if (menus is null)
                        menus = await http.GetFromJsonAsync<MenuModel[]>($"api/restaurante/obtenermenu/{RestauranteId}");

                    await GuardarDatosDeSesion(MesaId.Value, RestauranteId.Value);
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
    }
}
