using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Shared
{
    public class ItemCuenta
    {
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal Valor => Cantidad * ValorUnitario;
        public decimal ValorUnitario { get; set; }
    }
}
