using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositorio
{
    public class RepositorioConsumible : IRepositorioConsumible
    {
        private readonly Datos.Models.InfoOfRestaurantContext ctx;

        public RepositorioConsumible(Datos.Models.InfoOfRestaurantContext ctx)
        {
            this.ctx = ctx;  
        }

        public async Task Crear(int restauranteID, Consumible consumible)
        {
            var c = new Datos.Models.Consumible
            {
                Descripcion = consumible.Descripcion,
                Nombre = consumible.Nombre,
                Precio = consumible.Precio,
                RestauranteID = restauranteID,
                Imagen = consumible.Imagen
            };
            ctx.Consumible.Add(c);
            await ctx.SaveChangesAsync();
        }

        public async Task Editar(Consumible consumible)
        {
            var c = ctx.Consumible.FirstOrDefault(c => c.ConsumibleID == consumible.ConsumibleID);
            if (c is null)
                return;

            c.Nombre = consumible.Nombre;
            c.Descripcion = consumible.Descripcion;
            c.Precio = consumible.Precio;

            if(consumible.Imagen != null)
                c.Imagen = consumible.Imagen;

            await ctx.SaveChangesAsync();
        }

        public async Task Eliminar(int consumibleID)
        {
            var c = ctx.Consumible.FirstOrDefault(c => c.ConsumibleID == consumibleID);
            if (c is null)
                return;

            ctx.Consumible.Remove(c);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Consumible>> Listar(int restauranteID)
        {
            return await ctx.Consumible
                .Where(c => c.RestauranteID == restauranteID)
                .Select(c => new Consumible
                {
                    Descripcion = c.Descripcion,
                    Nombre = c.Nombre,
                    ConsumibleID = c.ConsumibleID,
                    Precio = c.Precio,
                    TieneFoto = c.Imagen != null
                }).ToListAsync();
        }

        public async Task<Consumible> Obtener(int consumibleID)
        {
            return await ctx.Consumible
                .Where(c => c.ConsumibleID == consumibleID)
                .Select(c => new Consumible { 
                    Descripcion = c.Descripcion,
                    Nombre = c.Nombre,
                    ConsumibleID = c.ConsumibleID,
                    Precio = c.Precio,
                    Imagen = c.Imagen
                })
                .FirstOrDefaultAsync();
        }

        public async Task<byte[]> ObtenerFoto(int consumibleID) {
            return await ctx.Consumible
                .Where(c => c.ConsumibleID == consumibleID)
                .Select(c => c.Imagen)
                .FirstOrDefaultAsync();
        }
    }
}
