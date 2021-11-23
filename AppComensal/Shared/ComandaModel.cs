using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppComensal.Shared
{
    public class ComandaModel
    {
        [Required]
        public int MesaId { get; set; }
        [Required]
        public int RestauranteId { get; set; }
        public List<Item> Items { get; set; }
    }
}
