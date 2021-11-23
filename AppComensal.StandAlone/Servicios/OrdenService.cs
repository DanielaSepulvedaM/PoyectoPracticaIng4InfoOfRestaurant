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
        private readonly RestauranteService restauranteSvc;

        public Cuenta Cuenta { get; internal set; }

        public OrdenService(HttpClient httpClient, RestauranteService restauranteSvc)
        {
            this.httpClient = httpClient;
            this.restauranteSvc = restauranteSvc;
            Cuenta = new Cuenta();
        }

        public async Task InitializeAync() {
            if(restauranteSvc.TodoListo)
                await ObtenerCuenta();
        }

        public async Task<ResponseModel> RealizarOrden(ComandaModel comanda) {
            comanda.RestauranteId = await restauranteSvc.ObtenerRestauranteId();
            comanda.MesaId = await restauranteSvc.ObtenerMesa();

            var res = await httpClient.PostAsJsonAsync("api/orden/registrarOrden", comanda);
            if (!res.IsSuccessStatusCode)
                return new OrdenResponseModel() { Mensaje = "No se pudo realizar la orden" };
            else
            {
                var response = await res.Content.ReadFromJsonAsync<ResponseModel>();
                if (response.Exitoso)
                    await ObtenerCuenta();

                return response;
            }
        }

        public async Task ObtenerCuenta()
        {
            var mesaId = await restauranteSvc.ObtenerMesa();
#pragma warning disable CS8601 // Possible null reference assignment.
            Cuenta = await httpClient.GetFromJsonAsync<Cuenta>($"api/orden/ObtenerCuenta?mesaId={mesaId}");
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        public async Task<ResponseModel> PagarCuenta(TarjetaDeCredito tarjeta) {
            var mesaId = await restauranteSvc.ObtenerMesa();

            var model = new PagoModel { 
                MesaId = mesaId,
                IncluirServicio = Cuenta.IncluirServicio,
                Tarjeta = tarjeta
            };

            var result = await httpClient.PostAsJsonAsync("api/orden/PagarCuenta", model);
            if (!result.IsSuccessStatusCode)
                return new ResponseModel { Mensaje = "Ocurrio un error al intentar procesar el pago, intenta de nuevo" };

            var resultadoPago = await result.Content.ReadFromJsonAsync<ResponseModel>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (resultadoPago.Exitoso)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                Cuenta = new Cuenta();

            return resultadoPago;
        }
    }
}
