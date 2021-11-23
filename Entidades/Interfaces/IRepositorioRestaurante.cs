using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepositorioRestaurante
    {
        IEnumerable<Restaurante> ListarRestaurantes();
        void CrearRestaurante(Restaurante restaurante);
        void EliminarRestaurante(int restauranteID);
        void EditarRestaurante(int restauranteID, Restaurante restaurante);
        Restaurante ObtenerRestaurante(int restauranteID);
    }
}
