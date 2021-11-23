using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IRepositorioCuenta
    {
        public Task<Cuenta> ObtenerCuentaAsync(int mesaId);
        public Task AgregarServicios(int cuentaId, List<Servicio> items);
        public ValueTask<int> ObtenerCuentaParaAgregarServicio(int restauranteId, int mesaId);
        public Task CerrarCuenta(Cuenta cuenta);
    }
}
