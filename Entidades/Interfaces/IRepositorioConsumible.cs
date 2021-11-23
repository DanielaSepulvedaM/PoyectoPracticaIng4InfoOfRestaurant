using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IRepositorioConsumible
    {
        Task<IEnumerable<Consumible>> Listar(int restauranteID);
        Task Crear(int restauranteID, Consumible consumible);
        Task Eliminar(int consumibleID);
        Task Editar(Consumible consumible);
        Task<Consumible> Obtener(int consumibleID);
        Task<byte[]> ObtenerFoto(int consumibleID);
    }
}
