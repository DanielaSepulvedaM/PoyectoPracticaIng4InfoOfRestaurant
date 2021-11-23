using Entidades;
using Entidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio
{
    public class RepositorioRestaurante : IRepositorioRestaurante, IRepositorioRestauranteComensales
    {
        private Datos.Models.InfoOfRestaurantContext Context;

        public RepositorioRestaurante(Datos.Models.InfoOfRestaurantContext ctx) {
            Context = ctx;
        }
        public void CrearRestaurante(Restaurante restaurante)
        {
            var rest = new Datos.Models.Restaurante
            {
                Barrio = restaurante.Barrio,
                Direccion = restaurante.Direccion,
                Nit = restaurante.Nit,
                Nombre = restaurante.Nombre,
                Telefono = restaurante.Telefono,
                Eliminado = false
            };
            Context.Restaurante.Add(rest);
            Context.SaveChanges();
        }
        public Restaurante ObtenerRestaurante(int restauranteID)
        {
            var restaurante = Context.Restaurante.FirstOrDefault(res => res.RestauranteID == restauranteID);
            if (restaurante == null)
                return null;
            return new Restaurante
            {
                Barrio = restaurante.Barrio,
                Direccion = restaurante.Direccion,
                Nit = restaurante.Nit,
                Nombre = restaurante.Nombre,
                RestauranteID = restaurante.RestauranteID,
                Telefono = restaurante.Telefono
            };
        }

        public void EditarRestaurante(int restauranteID, Restaurante restaurante)
        {
            var obtRestaurante = Context.Restaurante.FirstOrDefault(res => res.RestauranteID == restauranteID);

            obtRestaurante.Barrio = restaurante.Barrio;
            obtRestaurante.Direccion = restaurante.Direccion;
            obtRestaurante.Nit = restaurante.Nit;
            obtRestaurante.RestauranteID = restaurante.RestauranteID;
            obtRestaurante.Telefono = restaurante.Telefono;
            obtRestaurante.Nombre = restaurante.Nombre;

            Context.SaveChanges();
        }
        public void EliminarRestaurante(int restauranteID)
        {
            var rest = Context.Restaurante.FirstOrDefault(res => res.RestauranteID == restauranteID);//obtener de la BD
            if (rest != null)
            {
                rest.Eliminado = true;
                Context.SaveChanges();
            }
        }
        public IEnumerable<Restaurante> ListarRestaurantes()
        {    
           return Context.Restaurante.Where(r => r.Eliminado == false)
                                      .Select(r => new Restaurante { 
                Barrio = r.Barrio,
                Direccion = r.Direccion,
                Nit = r.Nit,
                Nombre = r.Nombre,
                Telefono = r.Telefono,
               RestauranteID = r.RestauranteID    
           }).ToList();
        }

        public List<Menu> ObtenerMenu(int restauranteID)
        {
            var datos = Context.Menu
                .Include(m => m.ConsumibleMenu)
                .ThenInclude(c => c.Consumible)
                .Where(m => m.RestauranteID == restauranteID)
                .ToList()
                .Select(m => new Entidades.Menu { 
                    Nombre = m.Nombre,
                    Consumibles = m.ConsumibleMenu.Select(cm => new Consumible { 
                        ConsumibleID = cm.ConsumibleID, 
                        Descripcion = cm.Consumible.Descripcion,
                        Nombre = cm.Consumible.Nombre,
                        Precio = cm.Consumible.Precio
                    }).ToList()
                });
            return datos.ToList();
        }
    }   
}
