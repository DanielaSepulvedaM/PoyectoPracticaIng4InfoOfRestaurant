using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Shared
{
    public class PagoModel
    {
        public TarjetaDeCredito Tarjeta { get; set; }
        public bool IncluirServicio { get; set; }
        public int MesaId { get; set; }
    }
}
