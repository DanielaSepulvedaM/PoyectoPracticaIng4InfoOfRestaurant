using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Menu
    {
        public Menu()
        {
            ConsumibleMenu = new HashSet<ConsumibleMenu>();
        }

        [Key]
        public int MenuID { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(150)]
        public string Descripcion { get; set; }
        public int RestauranteID { get; set; }

        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("Menu")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty("Menu")]
        public virtual ICollection<ConsumibleMenu> ConsumibleMenu { get; set; }
    }
}
