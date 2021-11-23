using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Factura
    {
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<ItemFactura> Items { get; set; }
    }
}
