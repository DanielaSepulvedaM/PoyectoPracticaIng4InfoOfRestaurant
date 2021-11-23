using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Orden
    {
        public int OrdenID { get; set; }
        public DateTime Fecha { get; set; }
        public Mesa Mesa { get; set; }
        public List<Servicio> Items { get; set; }
    }
}
