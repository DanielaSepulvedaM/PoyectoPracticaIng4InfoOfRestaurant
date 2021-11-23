using System;
using System.Collections.Generic;
using System.Linq;

namespace Entidades
{
    public class Cuenta
    {
        public int CuentaID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<Servicio> Items { get; set; }
        public bool Cerrada { get; set; }
        public decimal PorcentajePropina { get; set; } = 0.10M;
        public decimal Total { get; set; }
        public Mesa Mesa { get; set; }
        public Restaurante Restaurante { get; set; }
        public decimal SubTotal => Items.Sum(i => i.Precio);

        public string CodigoAprobacion { get; set; }
        public string MetodoDePago { get; set; }

        public decimal LiquidarTotal(bool IncluirServicio) => IncluirServicio ? SubTotal * 1 + PorcentajePropina : SubTotal;
    }
}
