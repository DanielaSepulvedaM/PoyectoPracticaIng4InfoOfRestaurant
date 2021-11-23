using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Models
{
    public class MenuViewModel
    {
        public int MenuID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }

        public List<int> ConsumiblesIds { get; set; }

        public MenuViewModel()
        {
            ConsumiblesIds = new List<int>();
        }
    }
}
