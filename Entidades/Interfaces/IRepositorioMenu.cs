using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepositorioMenu
    {
        List<Menu> Listar(int restauranteID);
        void Crear(Menu menu,int restauranteID);
        void Eliminar(int menuID);
        void Editar(Menu menu, int menuID);
        Menu Obtener(int menuID);
    }
}
