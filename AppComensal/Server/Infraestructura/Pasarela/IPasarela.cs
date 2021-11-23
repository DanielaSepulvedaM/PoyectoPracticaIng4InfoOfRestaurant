using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Pasarela
{
    public interface IPasarela
    {
        public Task<RespuestaPasarela> ProcesarPago(MensajePasarela mensajePasarela);
    }
}
