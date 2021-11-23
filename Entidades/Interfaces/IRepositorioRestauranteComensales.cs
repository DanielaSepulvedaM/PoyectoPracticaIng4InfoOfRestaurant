using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepositorioRestauranteComensales
    {
        List<Menu> ObtenerMenu(int restauranteID);
        Restaurante ObtenerRestaurante(int restauranteID);
    }
}
