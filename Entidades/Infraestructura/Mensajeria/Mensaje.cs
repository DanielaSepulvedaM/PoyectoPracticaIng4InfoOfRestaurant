using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Infraestructura.Mensajeria
{
    public class Mensaje
    {
        public const string LLAMAR_MESERO = "LlamarMesero";
        public const string PEDIR_CUENTA = "PedirCuenta";

        public int RestauranteId { get; set; }
        public string IdentificadorMesa { get; set; }
        public string Comando { get; set; }
        public DateTime HoraEnvio { get; set; } = DateTime.Now;
    }
}
