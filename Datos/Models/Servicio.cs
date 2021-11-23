using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Datos.Models
{
    public partial class Servicio
    {
        [Key]
        public int ServicioID { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [StringLength(100)]
        public string Peticiones { get; set; }
        [Column(TypeName = "decimal(12, 6)")]
        public decimal Precio { get; set; }
        public int ConsumibleID { get; set; }
        public int CuentaID { get; set; }

        [ForeignKey(nameof(ConsumibleID))]
        [InverseProperty("Servicio")]
        public virtual Consumible Consumible { get; set; }
        [ForeignKey(nameof(CuentaID))]
        [InverseProperty("Servicio")]
        public virtual Cuenta Cuenta { get; set; }
    }
}
