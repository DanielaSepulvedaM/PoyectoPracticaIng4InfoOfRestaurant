using Entidades;
using Entidades.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio
{
    public class RepositorioMenu : IRepositorioMenu
    {
        private readonly Datos.Models.InfoOfRestaurantContext context;

        public RepositorioMenu(Datos.Models.InfoOfRestaurantContext ctx)
        {
            context = ctx;
        }
        public List<Menu> Listar(int restauranteID)
        {
            return context.Menu
                .Where(m => m.RestauranteID == restauranteID)
                .Select(m => new Entidades.Menu {
                    MenuID = m.MenuID, 
                    Nombre = m.Nombre,
                    Descripcion = m.Descripcion,    
                }).ToList();
        }

        public void Editar(Menu menu, int menuID)
        {
            using var transaction = context.Database.BeginTransaction();

            var obtMenu = context.Menu.Include(c => c.ConsumibleMenu).FirstOrDefault(m => m.MenuID == menuID);
            if (obtMenu is null)
                return;

            obtMenu.Nombre = menu.Nombre;
            obtMenu.Descripcion = menu.Descripcion;
            context.SaveChanges();

            var consumiblesActuales = obtMenu.ConsumibleMenu; //context.ConsumibleMenu.Where(c => c.MenuID == menuID).ToList();

            var consumiblesIds = menu.Consumibles.ToDictionary(c => c.ConsumibleID);
            var consumiblesAEliminar = consumiblesActuales.Where(c => !consumiblesIds.ContainsKey(c.ConsumibleID));

            context.ConsumibleMenu.RemoveRange(consumiblesAEliminar);
            context.SaveChanges();

            var consumiblesActualesDic = obtMenu.ConsumibleMenu.ToDictionary(c => c.ConsumibleID);
            var consumiblesAInsertar = menu.Consumibles
                .Where(c => !consumiblesActualesDic.ContainsKey(c.ConsumibleID))
                .Select(c => new Datos.Models.ConsumibleMenu { ConsumibleID = c.ConsumibleID, MenuID = menuID });

            context.ConsumibleMenu.AddRange(consumiblesAInsertar);
            context.SaveChanges();

            transaction.Commit();

        }
       
        public Menu Obtener(int menuID)
        {
            var m = context.Menu.Include(m => m.ConsumibleMenu).FirstOrDefault(m => m.MenuID == menuID);
            if (m == null)
                return null;

            var consumibles = (from con in context.Consumible
                               join conmen in context.ConsumibleMenu on con.ConsumibleID equals conmen.ConsumibleID
                               where conmen.MenuID == menuID
                               select new Entidades.Consumible
                               {
                                   ConsumibleID = con.ConsumibleID,
                                   Descripcion = con.Descripcion,
                                   Nombre = con.Nombre,
                                   Precio = con.Precio
                               })
                               .ToList();

            return new Menu
            {
                MenuID = m.MenuID,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                Consumibles = consumibles,
                Restaurante = new Restaurante { RestauranteID = m.RestauranteID }
            };
        }

        public void Crear(Menu menu, int restauranteID)
        {
            using var transaction = context.Database.BeginTransaction();

            var men = new Datos.Models.Menu
            {
                Nombre = menu.Nombre,
                Descripcion = menu.Descripcion,
                RestauranteID = restauranteID
            };

            context.Menu.Add(men);
            context.SaveChanges();

            if (menu.Consumibles.Any())
            {
                var Consumibles = menu.Consumibles.Select(c => new Datos.Models.ConsumibleMenu { ConsumibleID = c.ConsumibleID, MenuID = men.MenuID }).ToArray();
                context.ConsumibleMenu.AddRange(Consumibles);
                context.SaveChanges();
            }

            transaction.Commit();
        }

        public void Eliminar(int menuID)
        {
            var registro = context.Menu.Include(m => m.ConsumibleMenu).FirstOrDefault(m => m.MenuID == menuID);
            if (registro == null)
                return;

            context.Menu.Remove(registro);
            context.SaveChanges();
        }
    }
}
