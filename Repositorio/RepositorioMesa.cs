using Datos.Models;
using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio
{
    public class RepositorioMesa : IRepositorioMesa
    {
        private readonly InfoOfRestaurantContext context;

        public RepositorioMesa(InfoOfRestaurantContext context)
        {
            this.context = context;
        }

        public void Crear(Entidades.Mesa model)
        {
            var mesa = new Datos.Models.Mesa
            {
                Identificador = model.Identificador,
                RestauranteID = model.Restaurante.RestauranteID
            };

            context.Mesa.Add(mesa);
            context.SaveChanges();
        }

        public void Editar(int id, Entidades.Mesa model)
        {
            var mesa = context.Mesa.FirstOrDefault(m => m.MesaID == id);
            if (mesa is null)
                return;

            mesa.Identificador = model.Identificador;
            context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var mesa = context.Mesa.FirstOrDefault(m => m.MesaID == id);
            if (mesa is null)
                return;

            context.Mesa.Remove(mesa);
            context.SaveChanges();
        }

        public IEnumerable<Entidades.Mesa> Listar(int restauranteID)
        {
            return context.Mesa
                .Where(m => m.RestauranteID == restauranteID)
                .Select(m => new Entidades.Mesa { 
                    Identificador = m.Identificador,
                    MesaId = m.MesaID,
                    Restaurante = new Entidades.Restaurante { 
                        RestauranteID = m.RestauranteID
                    }
                })
                .ToList();
        }

        public Entidades.Mesa Obtener(int id)
        {
            return context.Mesa
                .Where(m => m.MesaID == id)
                .Select(m => new Entidades.Mesa
                {
                    Identificador = m.Identificador,
                    MesaId = m.MesaID,
                    Restaurante = new Entidades.Restaurante
                    {
                        RestauranteID = m.RestauranteID
                    }
                })
                .FirstOrDefault();
        }
    }
}
