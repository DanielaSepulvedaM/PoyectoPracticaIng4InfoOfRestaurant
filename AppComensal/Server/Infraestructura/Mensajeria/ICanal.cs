using Entidades.Infraestructura.Mensajeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Mensajeria
{
    public interface ICanal
    {
        public Task EnviarMensaje(Mensaje mensaje);
    }
}
