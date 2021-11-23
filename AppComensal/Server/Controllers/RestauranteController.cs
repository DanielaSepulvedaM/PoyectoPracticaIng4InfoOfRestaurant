using AppComensal.Server.Infraestructura.Mensajeria;
using AppComensal.Shared;
using Entidades.Infraestructura.Mensajeria;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly IRepositorioRestauranteComensales repositorioRestaurante;
        private readonly ILogger<RestauranteController> logger;
        private readonly IRepositorioMesa repositorioMesa;

        public RestauranteController(IRepositorioRestauranteComensales repositorioRestaurante, ILogger<RestauranteController> logger, IRepositorioMesa repositorioMesa)
        {
            this.repositorioRestaurante = repositorioRestaurante;
            this.logger = logger;
            this.repositorioMesa = repositorioMesa;
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerMenu(int id)
        {
            var menus = this.repositorioRestaurante.ObtenerMenu(id);
          
            var obj = menus.Select(m => new MenuModel
            {
                Nombre = m.Nombre,
                Consumibles = m.Consumibles.Select(c => new ConsumibleModel
                {
                    ConsumibleID = c.ConsumibleID,
                    Descripcion = c.Descripcion,
                    Nombre = c.Nombre,
                    Precio = c.Precio
                })
            });

            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerInformacion(int id) {
            var rest = this.repositorioRestaurante.ObtenerRestaurante(id);
            var obj = new RestauranteModel
            {
                Nombre = rest.Nombre
            };

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> LlamarMesero([FromServices]ICanal canal, MensajeModel mensajeModel) {
            try
            {
                var mensaje = ArmarMensaje(mensajeModel);
                mensaje.Comando = Mensaje.LLAMAR_MESERO;

                await canal.EnviarMensaje(mensaje);

                return Ok(new ResponseModel { Exitoso = true });
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, "Error en el canal");
                return Ok(new ResponseModel { Mensaje = "Oops! algo salio mal llamando al mesero. Intentalo de nuevo o por favor levanta la mano." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PedirCuenta([FromServices] ICanal canal, MensajeModel mensajeModel) {
            try
            {
                var mensaje = ArmarMensaje(mensajeModel);
                mensaje.Comando = Mensaje.PEDIR_CUENTA;

                await canal.EnviarMensaje(mensaje);
                return Ok(new ResponseModel { Exitoso = true });
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, "Error en el canal");
                return Ok(new ResponseModel { Mensaje = "Oops! algo salio mal llamando al mesero. Intentalo de nuevo o por favor levanta la mano." });
            }
        }

        private Mensaje ArmarMensaje(MensajeModel mensajeModel) {
            var identificadorMesa = repositorioMesa.Obtener(mensajeModel.MesaId);
            var mensaje = new Mensaje
            {
                RestauranteId = identificadorMesa.Restaurante.RestauranteID,
                IdentificadorMesa = identificadorMesa.Identificador,
            };

            return mensaje;
        }
    }
}
