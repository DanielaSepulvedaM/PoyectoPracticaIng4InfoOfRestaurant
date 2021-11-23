using Entidades;
using System.Collections.Generic;

namespace Entidades.Interfaces
{
    public interface IRepositorioFuncionario
    {
        void AsociarRestaurante(int restauranteID, string funcionarioId);
        Restaurante ObtenerRestaurante(string funcionarioID);
        void DesasociarRestaurante(int restauranteID, string funcionarioID);
        List<string> Listar(int restauranteID);
    }
}
