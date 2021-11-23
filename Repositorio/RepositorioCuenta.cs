using Datos.Models;
using Entidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioCuenta : IRepositorioCuenta
    {
        private readonly InfoOfRestaurantContext context;

        public RepositorioCuenta(InfoOfRestaurantContext context)
        {
            this.context = context;
        }

        public async ValueTask<int> ObtenerCuentaParaAgregarServicio(int restauranteId, int mesaId) {
            var cuenta = await context.Cuenta.FirstOrDefaultAsync(c => c.MesaID == mesaId && !c.Cerrada);
            if (cuenta != null)
                return cuenta.CuentaID;

            cuenta = new Cuenta
            {
                FechaCreacion = DateTime.Today,
                MesaID = mesaId,
                RestauranteID = restauranteId
            };

            context.Cuenta.Add(cuenta);
            await context.SaveChangesAsync();

            return cuenta.CuentaID;
        }

        public async Task<Entidades.Cuenta> ObtenerCuentaAsync(int mesaId)
        {
            var record = await context.Cuenta.Include(c => c.Servicio).Where(c => c.MesaID == mesaId && c.Cerrada == false).FirstOrDefaultAsync();
            if (record is null)
                return null;

            var cuenta = new Entidades.Cuenta { 
                CuentaID = record.CuentaID,
                Cerrada = record.Cerrada,
                FechaCreacion = record.FechaCreacion,
                Items = record.Servicio.Select(s => new Entidades.Servicio { 
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    Consumible = new Entidades.Consumible { ConsumibleID = s.ConsumibleID }
                }).ToList()
            };

            return cuenta;
        }

        public async Task AgregarServicios(int cuentaId, List<Entidades.Servicio> items)
        {
            var servicios = items.Select(i => new Servicio
            {
                Nombre = i.Nombre,
                Descripcion = i.Descripcion,
                Peticiones = i.Peticiones,
                ConsumibleID = i.Consumible.ConsumibleID,
                Precio = i.Precio,
                CuentaID = cuentaId
            }).ToList();
            context.Servicio.AddRange(servicios);
            await context.SaveChangesAsync();
        }

        public async Task CerrarCuenta(Entidades.Cuenta cuenta)
        {
            var cuentaRecord = context.Cuenta.First(c => c.CuentaID == cuenta.CuentaID);
            cuentaRecord.CodigoDeAprobacion = cuenta.CodigoAprobacion;
            cuentaRecord.MetodoDePago = cuenta.MetodoDePago;
            cuentaRecord.Cerrada = true;
            await context.SaveChangesAsync();
        }
    }
}
