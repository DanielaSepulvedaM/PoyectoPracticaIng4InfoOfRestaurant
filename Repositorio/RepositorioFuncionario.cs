using Datos.Models;
using Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio
{
    public class RepositorioFuncionario : IRepositorioFuncionario
    {
        private Datos.Models.InfoOfRestaurantContext context;
        public RepositorioFuncionario(Datos.Models.InfoOfRestaurantContext ctx)
        {
            context = ctx;
        }

        public void AsociarRestaurante(int restauranteID, string funcionarioId)
        {
            var registro = new FuncionarioRestaurante {
                RestauranteID = restauranteID,
                FuncionarioID = funcionarioId
            };
            context.FuncionarioRestaurante.Add(registro);
            context.SaveChanges();
        }

        public void DesasociarRestaurante(int restauranteID, string funcionarioID)
        {
            var registro = context.FuncionarioRestaurante.FirstOrDefault(f => f.RestauranteID == restauranteID && f.FuncionarioID == funcionarioID);
            if (registro == null)
                return;

            context.FuncionarioRestaurante.Remove(registro);
            context.SaveChanges();
        }

        public List<string> Listar(int restauranteID)
        {
            return context.FuncionarioRestaurante.Where(f => f.RestauranteID == restauranteID).Select(f => f.FuncionarioID).ToList();
        }

        public Entidades.Restaurante ObtenerRestaurante(string funcionarioID)
        {
            var r = context.FuncionarioRestaurante.FirstOrDefault(f => f.FuncionarioID == funcionarioID);
            if (r == null)
                return null;

            return new Entidades.Restaurante
            {
                RestauranteID = r.RestauranteID
            };
        }
    }
}
