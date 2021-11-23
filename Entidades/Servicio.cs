using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Servicio
    {
        public int ServicioID { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Peticiones { get; set; }
        public Consumible Consumible { get; set; }
    }
}
