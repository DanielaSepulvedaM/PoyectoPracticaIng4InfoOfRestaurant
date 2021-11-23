using AppComensal.Server.Infraestructura.Pasarela;
using AppComensal.Shared;
using Datos.Models;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IRepositorioCuenta repositorioCuenta;
        private readonly ILogger<OrdenController> logger;

        public OrdenController(IRepositorioCuenta repositorioCuenta, ILogger<OrdenController> logger)
        {
            this.repositorioCuenta = repositorioCuenta;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarOrden([FromServices]InfoOfRestaurantContext context, ComandaModel comandaModel) {
            if (!ModelState.IsValid)
                return BadRequest();

            using var transaction = context.Database.BeginTransaction();
            try
            {
                var cuentaId = await repositorioCuenta.ObtenerCuentaParaAgregarServicio(comandaModel.RestauranteId, comandaModel.MesaId);
                var Items = comandaModel.Items.Select(i => new Entidades.Servicio
                {
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion,
                    Precio = i.Precio,
                    Peticiones = i.Nota,
                    Consumible = new Entidades.Consumible{ ConsumibleID = i.ConsumibleID }
                }).ToList();
                await repositorioCuenta.AgregarServicios(cuentaId, Items);
                transaction.Commit();

                return Ok(new ResponseModel { Exitoso = true });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante creacion de orden", comandaModel);
                return Ok(new ResponseModel { Mensaje = "Lo sentimos, ocurrio un error creando tu orden." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCuenta(int mesaId) {
            var cuenta = await repositorioCuenta.ObtenerCuentaAsync(mesaId);
            if (cuenta is null)
                return Ok(new Shared.Cuenta());

            var items = cuenta.Items.GroupBy(i => i.Consumible.ConsumibleID, (g, items) => new ItemCuenta { Cantidad = items.Count(), Nombre = items.First().Nombre, ValorUnitario = items.First().Precio }).ToList();

            var obj = new Shared.Cuenta
            {
                SubTotal = items.Sum(i => i.Valor),
                Items = items,
                IncluirServicio = true
            };
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> PagarCuenta([FromServices]IPasarela pasarela, PagoModel model) {
            var cuenta = await repositorioCuenta.ObtenerCuentaAsync(model.MesaId);
            var total = cuenta.LiquidarTotal(model.IncluirServicio);

            var mensajePasarela = new MensajePasarela(
                cuenta.CuentaID,
                total,
                model.Tarjeta.Nombre,
                model.Tarjeta.Numero,
                model.Tarjeta.CVV,
                model.Tarjeta.Mes,
                model.Tarjeta.Año
            );

            var resultado = await pasarela.ProcesarPago(mensajePasarela);

            if (!resultado.Aprobado)
            {
                logger.LogInformation(resultado.Mensaje, mensajePasarela);
                return Ok(new ResponseModel { Mensaje = "No se pudo completar el pago" });
            }

            cuenta.Total = total;
            cuenta.MetodoDePago = "EnLinea";
            cuenta.CodigoAprobacion = resultado.CodigoAprobacion;

            logger.LogInformation("Pago aprobado", cuenta, mensajePasarela, resultado);

            await repositorioCuenta.CerrarCuenta(cuenta);

            return Ok(new ResponseModel { Exitoso = true, Mensaje = $"Numero de aprobacion: {resultado.CodigoAprobacion}" });
        }
    }
}
