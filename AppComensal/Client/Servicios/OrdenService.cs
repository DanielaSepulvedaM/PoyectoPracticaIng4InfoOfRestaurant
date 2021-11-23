using AppComensal.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppComensal.Client.Servicios
{
    public class OrdenService
    {
        private readonly HttpClient httpClient;

        public OrdenService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseModel> RealizarOrden(ComandaModel comanda) {
            var res = await httpClient.PostAsJsonAsync("api/orden/crearOrden", comanda);
            if (!res.IsSuccessStatusCode)
                return new OrdenResponseModel() { Mensaje = "No se pudo crear la orden" };

            return await res.Content.ReadFromJsonAsync<OrdenResponseModel>();
        }

        public async Task<Cuenta> ObtenerCuenta(int mesaId)
        {
            return await httpClient.GetFromJsonAsync<Cuenta>($"api/orden/ObtenerCuenta?mesaId={mesaId}");
        }
    }
}
