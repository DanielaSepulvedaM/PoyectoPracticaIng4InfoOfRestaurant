using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Shared
{
    public class MenuModel
    {
        public string Nombre { get; set; }
        public IEnumerable<ConsumibleModel> Consumibles { get; set; }
    }

    public class ConsumibleModel {
        public int ConsumibleID { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
