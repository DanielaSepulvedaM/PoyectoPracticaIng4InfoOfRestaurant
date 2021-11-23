using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Pasarela
{
    public class RespuestaPasarela
    {
        public bool Aprobado { get; set; }
        public string Mensaje { get; set; }
        public string CodigoAprobacion { get; set; }
    }
}
