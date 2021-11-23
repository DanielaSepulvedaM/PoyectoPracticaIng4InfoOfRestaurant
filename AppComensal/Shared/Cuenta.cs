using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Shared
{
    public class Cuenta
    {
        public List<ItemCuenta> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal PorcentajePropina { get; } = 0.10M;
        public decimal Propina => SubTotal * PorcentajePropina;
        public bool IncluirServicio { get; set; }
        public decimal Total => IncluirServicio ? SubTotal + Propina : SubTotal;
    }
}
