using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Consumible
    {
        public Consumible()
        {
            ConsumibleMenu = new HashSet<ConsumibleMenu>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int ConsumibleID { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Precio { get; set; }
        [Column(TypeName = "image")]
        public byte[] Imagen { get; set; }
        public int RestauranteID { get; set; }

        [ForeignKey(nameof(RestauranteID))]
        [InverseProperty("Consumible")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty("Consumible")]
        public virtual ICollection<ConsumibleMenu> ConsumibleMenu { get; set; }
        [InverseProperty("Consumible")]
        public virtual ICollection<Servicio> Servicio { get; set; }
    }
}
