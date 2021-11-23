using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepositorioMesa
    {
        IEnumerable<Mesa> Listar(int restauranteID);
        void Crear(Mesa model);
        Mesa Obtener(int id);
        void Editar(int id, Mesa model);
        void Eliminar(int id);
    }
}
